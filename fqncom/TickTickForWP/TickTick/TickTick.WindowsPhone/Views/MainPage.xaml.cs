using NotificationsExtensions.ToastContent;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Common;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Utilities;
using TickTick.ViewModels;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using TickTick.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region 自定义属性
        /// <summary>
        /// 视图模型
        /// </summary>
        public MainPageViewModel ViewModel { get; set; }

        #endregion

        private bool IsFirstComing = true;
        public MainPage()
        {
            IsFirstComing = true;
            ViewModel = new MainPageViewModel();

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            InitialMainPageObjectAndEvent();

        }

        private void InitialMainPageObjectAndEvent()
        {
            DrawerLayout.InitializeDrawerLayout();
            DrawerLayout.DrawerClosed += DrawerLayout_DrawerClosed;
            DrawerLayout.DrawerOpened += DrawerLayout_DrawerClosed;

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed_MainPage;
        }

        private void DrawerLayout_DrawerClosed(object sender)
        {
            this.BottomAppBar.Visibility = DrawerLayout.IsDrawerOpen ? Windows.UI.Xaml.Visibility.Collapsed : Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            this.Frame.BackStack.Clear();

            //初始化用户设置
            await ViewModel.GetUserProfile();

            await CheckUserModelAndShowMessage();

            // 启动应用程序进行同步操作
            if (IsFirstComing)
            {
                await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
                IsFirstComing = false;
            }

            var param = e.Parameter as FrameTransitionParam;

            if (param != null)
            {
                await DealWithFrameTransitionParam(param);
            }
        }

        private async Task CheckUserModelAndShowMessage()
        {

            var defaultSignUserInfo = User.GetDefaultSignUserInfo();
            //判断用户是否为空，或者是否是临时用户

            if (App.SignUserInfo == null)
            {
                App.SignUserInfo = defaultSignUserInfo;
            }
            if (string.Equals(App.SignUserInfo.Sid, User.LOCAL_MODE_ID))
            {
                ViewModel.UserInfo = defaultSignUserInfo;
                await MessageDialogHelper.MessageDialogShowAsync("您当前是临时用户，您的数据无法进行同步，建议您立即登入，以保证数据不会丢失。", "提示");
            }
            else
            {
                ViewModel.UserInfo = App.SignUserInfo;
            }
        }

        private async Task DealWithFrameTransitionParam(FrameTransitionParam param)
        {
            if (param != null)
            {
                if (param.TasksIdFromToast != null)
                {
                    var toastTasks = ViewModel.Tasks.FirstOrDefault(t => t.Id == param.TasksIdFromToast);
                    if (toastTasks == null)
                    {
                        await MessageDialogHelper.MessageDialogShowAsync("该任务已在其他端被删除", "提示");
                    }
                    else
                    {
                        ViewModel.ToastTasks = toastTasks;
                        this.txtToastTasksTitle.Text = ViewModel.ToastTasks.Title;
                        this.txtToastTasksDueTime.Text = ViewModel.ToastTasks.DueDate.ToString();
                        this.cmbToastTasksSnooze.ItemsSource = SelectionListEnum.SnoozeBackTimeSelectionList;
                        this.cmbToastTasksSnooze.DisplayMemberPath = "Name";
                        this.cmbToastTasksSnooze.SelectedValuePath = "SnoozeBackValue";

                        if (this.cmbToastTasksSnooze.SelectedIndex == -1)
                        {
                            this.cmbToastTasksSnooze.SelectedIndex = 0;
                        }

                        this.popupToastTasksDetail.Width = Window.Current.Bounds.Width;
                        this.popupToastTasksDetail.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.popupToastTasksDetail.IsOpen = true;
                    }
                }
                //从修改页面传过来的数据，为了不再从数据库重新查一次。
                if (param.NewTasks != null)
                {
                    ViewModel.Tasks.Add(param.NewTasks);
                }
                if (param.UpdateTasks != null)
                {
                    var oldTasks = ViewModel.Tasks.FirstOrDefault(t => t.Id == param.UpdateTasks.Id);
                    if (oldTasks != null)
                    {
                        ViewModel.Tasks.Remove(oldTasks);
                    }
                    ViewModel.Tasks.Insert(0, param.UpdateTasks);
                }
            }
        }
       
        public void HardwareButtons_BackPressed_MainPage(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
                e.Handled = true;
            }
            else
            {
                //e.Handled = true;
                //Application.Current.Exit();
            }
            //HardwareButtons.BackPressed -= HardwareButtons_BackPressed_MainPage;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async Task InitialAsyncAndBindingResource(TasksSortEnum tasksSortEnum)
        {
            // 同步操作,消耗的时间比较多
            // 获取所有内容
            this.progressRing.IsActive = true;
            if (App.SignUserInfo.Sid != User.LOCAL_MODE_ID)
            {
                await ViewModel.InitialAsync();
            }
            await ViewModel.LoadData(tasksSortEnum);

            if (ViewModel.TasksFinished.Count > 0)
            {
                this.listViewTasksFinished.Visibility = Visibility.Visible;
            }
            this.progressRing.IsActive = false;

            AddTasksReminderIntoToastNotification();
        }


        /// <summary>
        /// 将任务提醒添加进toast通知
        /// </summary>
        private async void AddTasksReminderIntoToastNotification()
        {
            //var toastList = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            // 先清删除，再添加
            ToastNotificationManager.History.Clear();

            var tasksNeedNotification = ViewModel.TasksNeedNotification;
            if (tasksNeedNotification.Count == 0) 
            {
                return;
            }

            var toastTmpl = ToastContentFactory.CreateToastImageAndText03();

            //后台开始进行提醒的创建
            foreach (Tasks item in tasksNeedNotification)
            {
                if (item.SnoozeRemindTime == null || item.SnoozeRemindTime.Value < DateTime.UtcNow)
                {
                    if (LoggerHelper.IS_LOG_ENABLED)
                    {
                        await LoggerHelper.LogToAllChannels(null, string.Format("创建提醒的时候该item：{0}没有设置remindertime", item.Title));
                    }
                    continue;
                }
                //((XmlElement)imgNode).SetAttribute("src", "ms-appx:///Assets/Images/avatar.jpg");
                //textNodeList[0].InnerText = item.Title;
                //textNodeList[1].InnerText = item.Content;
                toastTmpl.BaseUri = "ms-appx:///";
                toastTmpl.Image.Src = "Assets/Images/avatar.jpg";
                toastTmpl.Image.Alt = "logo";
                toastTmpl.TextHeadingWrap.Text = item.Title;
                toastTmpl.TextBody.Text = item.Content;
                toastTmpl.Audio.Content = ToastAudioContent.IM;
                toastTmpl.Audio.Loop = false;
                toastTmpl.Duration = ToastDuration.Long;
                toastTmpl.Launch = string.Format("/MainPage.xaml?tasksId={0}", item.Id);
                toastTmpl.StrictValidation = true;


                //IXmlNode toastNode = toastTmpl.SelectSingleNode("/toast");
                //XmlElement audioNode = toastTmpl.CreateElement("audio");
                //audioNode.SetAttribute("src", "ms-winsoundevent:Notification.IM");
                //toastNode.AppendChild(audioNode);

                // toast duration
                //((XmlElement)toastNode).SetAttribute("duration", "short");
                //((XmlElement)toastNode).SetAttribute("tag", string.Format("id:{0}", item.Id));

                // toast navigation
                ////var toastNavigationUriString = string.Format("#/TasksDetailPageSimple.xaml?param1={0}", new FrameTransitionParam { Tasks = item });
                //var toastNavigationUriString = string.Format("{\"type\":\"toast\",\"param1\":\"{0}\"}", item.Id);
                //var toastElement = ((XmlElement)toastTmpl.SelectSingleNode("/toast"));
                //toastElement.SetAttribute("launch", toastNavigationUriString);

                #region 添加按钮的xml代码，支持wp10，不支持wp8.1
                //string toastXmlString =
                //    "<toast duration=\"long\">\n" +
                //        "<visual>\n" +
                //            "<binding template=\"ToastText02\">\n" +
                //                "<text id=\"1\">Alarms Notifications SDK Sample App</text>\n" +
                //                "<text id=\"2\">" + "ceshi alarm" + "</text>\n" +
                //            "</binding>\n" +
                //        "</visual>\n" +
                //        "<commands scenario=\"alarm\">\n" +
                //            "<command id=\"snooze\"/>\n" +
                //            "<command id=\"dismiss\"/>\n" +
                //        "</commands>\n" +
                //        "<audio src=\"ms-winsoundevent:Notification.Looping.Alarm2\" loop=\"true\" />\n" +
                //    "</toast>\n"; 
                #endregion

                ScheduledToastNotification toast = new ScheduledToastNotification(toastTmpl.GetXml(), item.SnoozeRemindTime.Value.ToLocalTime());
                toast.Tag = string.Format("id:{0}", item.Id);

                // 不根据特定的id去删除，再添加，而是一次性全部删除，然后再添加新的，从而防止之前添加的但是后来取消的任务继续提醒。
                ToastNotificationManager.History.Remove(string.Format("id:{0}", item.Id));
                //foreach (var schedule in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
                //{

                //}
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            }
        }



        /// <summary>
        /// 点击projects列表项触发，显示对应task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ProjectsListItem_Clicked(object sender, ItemClickEventArgs e)
        {
            //获取点击的project对象
            var project = e.ClickedItem as Projects;
            if (project == null)
            {
                return;
            }
            //将当前选择的project放到主界面的tag中去
            //this.gridMain.Tag = project;
            ViewModel.ProjectsSelected = project;

            this.txtMainTitle.Text = project.Name;
            DrawerLayout.CloseDrawer();
            //根据特定的projectId来获取对应的tasks列表
            await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
        }

        ///// <summary>
        ///// 点击任务列表，跳转taskdetail页面
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TasksListItem_Clicked(object sender, ItemClickEventArgs e)
        //{
        //    //与修改要有一个标识进行区分,标识就是判断是否存在tasks
        //    FrameTransitionParam param = new FrameTransitionParam { Projects = ViewModel.ProjectsSelected as Projects, Tasks = e.ClickedItem as Tasks };

        //    NavigateHelper.NavigateToPageWithParam(typeof(TasksDetailPageSimple), param);
        //}


        /// <summary>
        /// 点击新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarAdd_Clicked(object sender, RoutedEventArgs e)
        {
            //与修改要有一个标识进行区分,可以创建一个临时对象，进行数据传递
            FrameTransitionParam param = new FrameTransitionParam { Projects = ViewModel.ProjectsSelected as Projects };

            NavigateHelper.NavigateToPageWithParam(typeof(TasksDetailPageSimple), param);
        }

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
            }
            else
            {
                DrawerLayout.OpenDrawer();
            }
        }

        /// <summary>
        /// projects列表页的同步按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarBtnAsync_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // TODO 最好启动一个动画
            await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
        }
        /// <summary>
        /// projects列表页设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarBtnSetting_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            NavigateHelper.NavigateToPage(typeof(SettingPage));

        }

        /// <summary>
        /// 显示已完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarShowComplete_Clicked(object sender, RoutedEventArgs e)
        {
            var appBarBtn = sender as AppBarButton;
            if (this.listViewTasksFinished.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                appBarBtn.Label = "隐藏已完成";
                this.listViewTasksFinished.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                appBarBtn.Label = "显示已完成";
                this.listViewTasksFinished.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarSort_Clicked(object sender, RoutedEventArgs e)
        {
            this.listViewSort.ItemsSource = new List<TasksSortEnum> 
            { 
                TasksSortEnum.Custom_Sort, 
                TasksSortEnum.DateTime_Sort, 
                TasksSortEnum.Title_Sort, 
                TasksSortEnum.Priorities_Sort
            };
            //var elementValue = this.gridMain as FrameworkElement;
            //var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue);

            this.listViewSort.ShowAt(this.gridMain);
        }
        /// <summary>
        /// 选择排序项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void SortListItem_Picked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            var tasksSortEnum = (TasksSortEnum)args.AddedItems.FirstOrDefault();
            await InitialAsyncAndBindingResource(tasksSortEnum);

        }

        private async void AppBarEditProject_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ProjectsSelected.IsIntelligentProjects)
            {
                await MessageDialogHelper.MessageDialogShowAsync("无法编辑智能清单", "提示");
                return;
            }
            IsAddNewProjects = false;
            this.pickerFlyoutEditProject.ShowAt(this.gridMenu);
        }

        private bool IsAddNewProjects = false;
        /// <summary>
        /// 确认修改projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnConfirmEditProject_Clicked(object sender, RoutedEventArgs e)
        {
            var editProjectName = this.txtEditProjectName.Text;
            if (string.IsNullOrEmpty(editProjectName))
            {
                await MessageDialogHelper.MessageDialogShowAsync("清单名称不能为空", "提示");
                //this.pickerFlyoutEditProject.Hide();
                return;
            }
            this.txtMainTitle.Text = editProjectName;
            this.pickerFlyoutEditProject.Hide();
            if (IsAddNewProjects)
            {
                var project = new Projects { Name = editProjectName };
                await ViewModel.AddProjects(project);
                ViewModel.ProjectsSelected = project;
                await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
            }
            else
            {
                var project = ViewModel.ProjectsSelected;
                project.Name = editProjectName;
                await ViewModel.UpdateProjects(project);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleleProject_Clicked(object sender, RoutedEventArgs e)
        {
            var confirm = new UICommand("确认")
            {
                Invoked = async (c) =>
                {
                    var project = ViewModel.ProjectsSelected;
                    await ViewModel.DeleteProjects(project);
                    ViewModel.ProjectsSelected = Entity.Projects.GetDefaultProjects();
                    await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
                }
            };
            var cancel = new UICommand("取消")
            {
                Invoked = (c) => { return; }
            };
            await MessageDialogHelper.MessageDialogShowAsync("确认要删除该分类吗？（同时删除其下所有任务，但你可以在垃圾箱中找到他们）", "删除确认", confirm, cancel);
        }
        /// <summary>
        /// 单击添加清单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarBtnAddProjects_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IsAddNewProjects = true;
            this.txtEditProjectName.Text = string.Empty;
            this.pickerFlyoutEditProject.ShowAt(this.gridMenu);
            //this.stackAddProject.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        
        /// <summary>
        /// toastTasks点击完成之后内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToastTasksCompleted_Clicked(object sender, RoutedEventArgs e)
        {
            await ViewModel.FinishTask(ViewModel.ToastTasks);
            this.popupToastTasksDetail.IsOpen = false;
            await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
        }
        /// <summary>
        /// toastTasks点击推迟之后内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToastTasksSnooze_Clicked(object sender, RoutedEventArgs e)
        {
            var snoozeBackValue = this.cmbToastTasksSnooze.SelectedItem as SnoozeTimeSelection;
            if (snoozeBackValue == null || snoozeBackValue.SnoozeBackValue == null)
            {
                return;
            }
            //ViewModel.ToastTasks.ReminderTime = ViewModel.ToastTasks.ReminderTime.Value.AddMinutes(snoozeBackValue.SnoozeBackValue);
            await ViewModel.SnoozeBackReminderTime(snoozeBackValue.SnoozeBackValue);
            this.popupToastTasksDetail.IsOpen = false;
            await InitialAsyncAndBindingResource(TasksSortEnum.Custom_Sort);
        }

        private void ToastTasksPopup_Loaded(object sender, RoutedEventArgs e)
        {
            this.gridToastTasksDetail.Width = Window.Current.Bounds.Width;
        }

        private async void CheckBoxIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            var rectangle = sender as Rectangle;
            var task = rectangle.Tag as Tasks;
            if (task != null)
            {
                //数据库更新
                await ViewModel.FinishTask(task);
            }
        }

        private void TasksMain_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            return;
            //args.Handled = true;
            if (args.Phase != 0)
            {
                throw new Exception("not in phrase 0");
            }

            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            Rectangle rectTasksCheckBox = borderTasksRoot.FindName("rectTasksCheckBox") as Rectangle;
            TextBlock txtTasksTitle = borderTasksRoot.FindName("txtTasksTitle") as TextBlock;
            TextBlock txtTasksDueTime = borderTasksRoot.FindName("txtTasksDueTime") as TextBlock;
            Rectangle rectTasksReminder = borderTasksRoot.FindName("rectTasksReminder") as Rectangle;
            Rectangle rectTasksRepeat = borderTasksRoot.FindName("rectTasksRepeat") as Rectangle;
            Rectangle rectTasksNote = borderTasksRoot.FindName("rectTasksNote") as Rectangle;

            txtTasksTitle.Opacity = 1;
            rectTasksCheckBox.Opacity = 0;
            txtTasksDueTime.Opacity = 0;
            rectTasksReminder.Opacity = 0;
            rectTasksRepeat.Opacity = 0;
            rectTasksNote.Opacity = 0;

            args.RegisterUpdateCallback(ShowTasksCheckBox);
        }

        private void ShowTasksCheckBox(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;
            if (args.Phase != 1)
            {
                throw new Exception("not in phrase 0");
            }
            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            Rectangle rectTasksCheckBox = borderTasksRoot.FindName("rectTasksCheckBox") as Rectangle;
            rectTasksCheckBox.Opacity = 1;

            args.RegisterUpdateCallback(ShowTasksDueTime);
        }

        private void ShowTasksDueTime(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;
            if (args.Phase != 2)
            {
                throw new Exception("not in phrase 0");
            }
            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            TextBlock txtTasksDueTime = borderTasksRoot.FindName("txtTasksDueTime") as TextBlock;
            txtTasksDueTime.Opacity = 1;

            args.RegisterUpdateCallback(ShowTasksReminder);
        }

        private void ShowTasksReminder(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;
            if (args.Phase != 3)
            {
                throw new Exception("not in phrase 0");
            }
            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            Rectangle rectTasksReminder = borderTasksRoot.FindName("rectTasksReminder") as Rectangle;

            rectTasksReminder.Opacity = 1;

            args.RegisterUpdateCallback(ShowTasksRepeat);
        }

        private void ShowTasksRepeat(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;
            if (args.Phase != 4)
            {
                throw new Exception("not in phrase 0");
            }
            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            Rectangle rectTasksRepeat = borderTasksRoot.FindName("rectTasksRepeat") as Rectangle;

            rectTasksRepeat.Opacity = 1;
            args.RegisterUpdateCallback(ShowTasksRestInfo);
        }

        private void ShowTasksRestInfo(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;
            if (args.Phase != 5)
            {
                throw new Exception("not in phrase 0");
            }
            Border borderTasksRoot = args.ItemContainer.ContentTemplateRoot as Border;

            Rectangle rectTasksNote = borderTasksRoot.FindName("rectTasksNote") as Rectangle;

            rectTasksNote.Opacity = 1;
        }

        private void TasksListItemContent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            var taskItemControl = sender as TasksItemControl;
            if (taskItemControl == null)
            {
                return;
            }
            var tasksItem = taskItemControl.DataContext as Tasks;

            FrameTransitionParam param = new FrameTransitionParam { Tasks = tasksItem };

            NavigateHelper.NavigateToPageWithParam(typeof(TasksDetailPageSimple), param);
        }
        /// <summary>
        /// 以下方法命中两次，第一次是非惯性条件下的位移，第二次是惯性条件下的位移。
        /// ScrollViewer_OnViewChanging，ScrollViewer.VerticalOffset是滚动条的起始位置，e.FinalView.VerticalOffset是非惯性移动到的位置，
        /// 第二次的时候起始位置变成上一次非惯性移动的位置，而e.FinalView.VerticalOffset是惯性移动的位置，根据这几个值可以对scrollView的滚动方向进行判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewer_OnViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            if (!e.IsInertial)
            {
                var scrollView = sender as ScrollViewer;
                // 获得起始位置
                var yBegin = scrollView.VerticalOffset;
                // 获得结束位置
                var yEnd = e.FinalView.VerticalOffset;
                // 获取差值
                var yDelta = yEnd - yBegin;
                this.BottomAppBar.ClosedDisplayMode = yDelta > 0
                    ? AppBarClosedDisplayMode.Minimal
                    : AppBarClosedDisplayMode.Compact;
            }
        }
    }
}
