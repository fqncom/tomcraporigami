using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TickTick.Common;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Utilities;
using TickTick.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Notification.Management;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using TickTick.Enum;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TasksDetailPageSimple : Page
    {
        #region 自定义属性
        /// <summary>
        /// 导航过来是为了更新还是新增，true标识更新，false表示新增
        /// </summary>
        public bool IsNavigateForUpdate { get; set; }
        public TasksDetailPageViewModel ViewModel { get; set; }
        #endregion
        public TasksDetailPageSimple()
        {
            ViewModel = new TasksDetailPageViewModel();
            try
            {
                this.InitializeComponent();
            }
            catch (Exception e)
            {

                throw e;
            }
            //this.NavigationCacheMode = NavigationCacheMode.Required;

            HardwareButtons.BackPressed += HardwareButtons_BackPressed_DetailPage;
        }

        async void HardwareButtons_BackPressed_DetailPage(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            if (string.IsNullOrEmpty(ViewModel.Tasks.Title))
            {
                //var confirm = new UICommand("确认") { Invoked = async (e1) => { NavigateHelper.NavigateToPage(typeof(MainPage)); } };
                //var cancel = new UICommand("取消") { Invoked = async (e1) => { return; } };
                //await MessageDialogHelper.MessageDialogShowAsync("标题为空，不能创建。是否放弃创建？", "提示", confirm, cancel);
                NavigateHelper.NavigateToPage(typeof(MainPage));
                return;
            }
            //此处最好进行一个判断，是更新还是新增操作，然后进行相关的数据库操作
            Task taskNeedAwait = null;
            FrameTransitionParam param = new FrameTransitionParam();
            if (this.IsNavigateForUpdate)//true表示更新
            {
                // 修改的数据
                //ViewModel.Tasks.Content = this.txtContent.Text;
                param.UpdateTasks = ViewModel.Tasks;
                taskNeedAwait = ViewModel.UpdateTasks();
            }
            else//false 表示新增
            {
                param.NewTasks = ViewModel.Tasks;
                //ViewModel.Tasks.Title = this.txtContent.Text;
                //ViewModel.Tasks = tasks;
                taskNeedAwait = ViewModel.AddTasks();
            }
            //var frame = Window.Current.Content as Frame;
            //if (frame.Navigate(typeof(MainPage)))
            //{
            //    e.Handled = true;
            //}
            // 在跳转前取消注册
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed_DetailPage;
            NavigateHelper.NavigateToPageWithParam(typeof(MainPage), param);

            await taskNeedAwait;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //获取传输过来的projects
            var param = e.Parameter as FrameTransitionParam;
            if (param == null)
            {
                return;
            }

            ViewModel.Projects = param.Projects;

            //如果存在tasks表示当前是更新，如果不存在tasks，表示当前是新增
            if (param.Tasks != null)
            {
                //获取最新的projects和tasks
                await Task.WhenAll(ViewModel.GetTasksByTasksId(param.Tasks.Id), ViewModel.GetProjectsByProjectId());
                //await ViewModel.GetTasksByTasksId(param.Tasks.Id);
                //await ViewModel.GetProjectsByProjectId();

                this.IsNavigateForUpdate = true;
                //this.txtDueDate.Text = ViewModel.Tasks.DueDate.ToString("");

                // TODO 可能有问题
                ViewModel.BatchAddCheckListItems(ViewModel.Tasks.ChecklistItems);//.TrulyCheckListItems = new Library.TrulyObservableCollection<ChecklistItem>(ViewModel.Tasks.ChecklistItems);
                //切换显示样式：文本模式--列表模式
                SwitchTasksMode();
            }
            else
            {
                ViewModel.Tasks = new Tasks
                {
                    //Assignee = long.MinValue,
                    //CreatedTime = DateTime.UtcNow,
                    //Attachments = new List<Attachment>(),
                    //ChecklistItems = new List<ChecklistItem>(),
                    //CommentCount = int.MinValue,
                    //CompletedTime = DateTime.UtcNow,
                    //Content = this.txtTasksContent.Text,
                    //Creator = long.MinValue,
                    Deleted = ModelStatusEnum.DELETED_NO,
                    //DueDate = DateTime.UtcNow,
                    //Etag = string.Empty,
                    //Etimestamp = long.MinValue,
                    //GoogleId = string.Empty,
                    //HasAttachment = false,
                    Id = TickTick.Enums.Constants.EntityIdentifie.DEFAULT_TASK_ID,
                    //IsOwner = false,
                    Kind = TickTick.Enums.Constants.Kind.TEXT,
                    //LocalId = long.MinValue,
                    //Location = new Location(),
                    //ModifiedTime = DateTime.UtcNow,
                    //Priority = int.MinValue,
                    ProjectId = ViewModel.Projects.Id.ToString(),
                    Projects = ViewModel.Projects,
                    ProjectSid = ViewModel.Projects.SId,
                    //Reminder = string.Empty,
                    //SnoozeRemindTime = DateTime.UtcNow,
                    //RemindTime = DateTime.UtcNow,
                    //RepeatFirstDate = DateTime.UtcNow,
                    //RepeatFlag = string.Empty,
                    //RepeatFrom = string.Empty,
                    //RepeatReminderTime = DateTime.UtcNow,
                    //RepeatTaskId = string.Empty,
                    //SId = StringUtils.GenerateShortStringGuid(),
                    //SortOrder = long.MinValue,
                    //Status = int.MinValue,
                    //Tags = new HashSet<string>(),
                    TaskStatus = ModelStatusEnum.NOT_COMPLETED,//int.MinValue,
                    //TimeZone = string.Empty,
                    //Title = string.Empty,
                    //Type = int.MinValue,
                    //UserCount = int.MinValue,
                    UserId = App.SignUserInfo.Sid
                };
                this.IsNavigateForUpdate = false;
                this.txtTasksContent.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            ViewModel.OriginalTasks = ViewModel.Tasks;
            ViewModel.Tasks = ObjectCopier.Clone<Tasks>(ViewModel.OriginalTasks);
            ViewModel.Tasks.Id = ViewModel.OriginalTasks.Id;// TODO 临时解决方案。JsonIgnore的问题

            //初始化时间选择器和combobox内容。
            InitialTasksPickerAndCombobox();
        }


        private void InitialTasksPickerAndCombobox()
        {
            if (this.IsNavigateForUpdate)
            {
                if (ViewModel.Tasks.DueDate != null)
                {
                    this.dueDatePicker.Date = ViewModel.Tasks.DueDate.Value.ToLocalTime().Date;
                    this.dueTimePicker.Time = ViewModel.Tasks.DueDate.Value.ToLocalTime().TimeOfDay;
                }
                this.cmbTasksPriority.SelectedValue = ViewModel.Tasks.Priority;
                this.cmbTasksRemind.SelectedValue = ViewModel.Tasks.Reminder;
                //this.cmbTasksRepeat.SelectedValue = ViewModel.GetRepeatSelectedValue();
            }
            else
            {
                this.dueDatePicker.Opacity = 0.5;
                this.dueTimePicker.Opacity = 0.5;
                this.cmbTasksRemind.SelectedValue = MinuteIncrementEnum.No_Reminder;
                this.cmbTasksPriority.SelectedValue = PrioritiesEnum.NonePriorities;
            }
        }


        /// <summary>
        /// 点击任务删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarBtnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            var confirm = new UICommand("确认")
            {
                Invoked = async (c) =>
                {
                    await ViewModel.DeleteTasks();
                    NavigateHelper.NavigateToPage(typeof(MainPage));
                    //var frame = Window.Current.Content as Frame;
                    //frame.Navigate(typeof (MainPage));
                }
            };
            var cancel = new UICommand("取消") { Invoked = (c) => { return; } };

            await MessageDialogHelper.MessageDialogShowAsync("确认要删除吗？", "提示", cancel, confirm);
        }

        private void PrioritiesComboxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var prioritiesItem = e.AddedItems.FirstOrDefault() as PrioritySelection;
            if (prioritiesItem == null)
            {
                return;
            }
            ViewModel.ChangePriority(prioritiesItem);
        }

        private void RemindTimeComboxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems.FirstOrDefault() as SnoozeTimeSelection;
            if (selectedItem == null)
            {
                return;
            }
            ViewModel.ChangeRemindTime(selectedItem);
        }

        private async void DueDate_Changed(object sender, DatePickerValueChangedEventArgs e)
        {
            var newDate = e.NewDate;
            if (ViewModel.Tasks.DueDate == null)
            {
                ViewModel.ChangeDueDate(newDate.DateTime.Date);

                ChangeRemindCmbSelectionToDefault();
                return;
            }
            if (newDate == null || newDate < DateTime.Now.Date || e.NewDate.DateTime.Date.Equals(ViewModel.Tasks.DueDate.Value.ToLocalTime().Date))
            {
                //最好提示用户时间早于当前
                //await MessageDialogHelper.MessageDialogShowAsync("日期不得小于当前日期", "截止日期");
                //(sender as DatePicker).Date = e.OldDate;
                return;
            }

            ViewModel.ChangeDueDate(newDate.DateTime.Date);// TODO Utc时间

            ChangeRemindCmbSelectionToDefault();

        }

        private void DueTime_Changed(object sender, TimePickerValueChangedEventArgs e)
        {
            var newTime = e.NewTime;
            if (e.NewTime.Equals(e.OldTime))
            {
                return;
            }
            if (ViewModel.Tasks.DueDate == null)
            {
                ViewModel.ChangeDueTime(newTime);

                ChangeRemindCmbSelectionToDefault();
                return;
            }
            if (newTime == null || e.NewTime.Equals(ViewModel.Tasks.DueDate.Value.ToLocalTime().TimeOfDay))
            {
                // TODO 暂时不做任何操作
                //(sender as TimePicker).Time = e.OldTime;
                return;
            }
            ViewModel.ChangeDueTime(newTime);// TODO 此处时间有问题，Utc时间

            ChangeRemindCmbSelectionToDefault();
        }
        /// <summary>
        /// 提醒，选中默认的“在开始时间”
        /// </summary>
        private void ChangeRemindCmbSelectionToDefault()
        {
            // 一旦更改默认选中“在开始时间提醒”
            if (this.cmbTasksRemind.SelectedIndex == -1)
            {
                this.cmbTasksRemind.SelectedValue = MinuteIncrementEnum.At_StartTime;
            }
        }

        /// <summary>
        /// 移动列表选择项单击触发cmbTasksRemind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MovePicker_Picked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            var selectedItem = args.AddedItems.FirstOrDefault() as Projects;
            if (selectedItem == null)
            {
                return;
            }
            ViewModel.ChangeBelongProjectId(selectedItem.Id.ToString());

            this.listPickerFlyoutMoveToProjects.Hide();
        }

        /// <summary>
        /// 单击移动触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarMove_Clicked(object sender, RoutedEventArgs e)
        {
            this.listPickerFlyoutMoveToProjects.ItemsSource = await ViewModel.GetAllProjects();

            await this.listPickerFlyoutMoveToProjects.ShowAtAsync(this.gridMain);
        }

        private void SwithTaskMode_Clicked(object sender, RoutedEventArgs e)
        {

            ViewModel.SwitchTaskMode();


            SwitchTasksMode();

        }

        public bool IsSwitchChecklistMode = false;
        private void SwitchTasksMode()
        {
            if (ViewModel.Tasks.IsChecklistMode() && !IsSwitchChecklistMode)
            {
                this.txtCheckListItemTitle.Text = ViewModel.Tasks.Title;
                this.listViewTasksContentChecklist.ItemsSource = ViewModel.TrulyCheckListItems;

                this.appBarSwitchTasksMode.Label = "列表模式";
                this.appBarSwitchTasksMode.Icon = new BitmapIcon { UriSource = new Uri("ms-appx:///Assets/Images/Scale-100/ic_menu_check_list_light.png", UriKind.RelativeOrAbsolute) };
                this.txtTasksContent.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.stackPanelCheckList.Visibility = Windows.UI.Xaml.Visibility.Visible;

                IsSwitchChecklistMode = true;
            }
            else
            {
                if (string.IsNullOrEmpty(ViewModel.Tasks.Content))
                {
                    this.txtTasksContent.Text = ViewModel.Tasks.Title;
                }
                else
                {
                    this.txtTasksContent.Text = string.Format("{0}\r\n{1}", ViewModel.Tasks.Title, ViewModel.Tasks.Content);
                }

                this.appBarSwitchTasksMode.Label = "文本模式";
                this.appBarSwitchTasksMode.Icon = new SymbolIcon { Symbol = Symbol.AlignLeft };
                this.stackPanelCheckList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.txtTasksContent.Visibility = Windows.UI.Xaml.Visibility.Visible;

                IsSwitchChecklistMode = false;
            }
        }



        /// <summary>
        /// 当内容文本框失去焦点的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtContent_LostFocus(object sender, RoutedEventArgs e)
        {
            var content = this.txtTasksContent.Text;
            if (string.IsNullOrEmpty(content))
            {
                ViewModel.Tasks.Title = string.Empty;
                ViewModel.Tasks.Content = string.Empty;
                return;
            }
            if (!content.Contains("\r\n"))
            {
                ViewModel.Tasks.Title = content;
                ViewModel.Tasks.Content = string.Empty;
                return;
            }
            ViewModel.Tasks.Title = content.Substring(0, content.IndexOf("\r\n", 0, StringComparison.Ordinal));
            ViewModel.Tasks.Content = content.Substring(content.IndexOf("\r\n", 0, StringComparison.Ordinal));
        }
        public bool IsOnChecklistMode()
        {
            if (ViewModel.Tasks == null)
            {
                return false;
            }
            return ViewModel.Tasks.IsChecklistMode();
        }

        private void CheckListItemContent_GotFocus(object sender, RoutedEventArgs e)
        {
            IsCheckListItemGotFocus = true;
            var textBlock = sender as TextBox;
            if (textBlock == null || textBlock.Parent == null)
            {
                return;
            }
            var rectangle = (textBlock.Parent as Grid).Children.LastOrDefault(t => t is Rectangle) as Rectangle;
            if (rectangle == null || !IsCheckListItemGotFocus)
            {
                return;
            }
            var image = new ImageBrush
            {
                ImageSource =
                    new BitmapImage(new Uri("ms-appx:///Assets/Images/Scale-100/task_popup_dismiss_icon_light.png"))
            };
            rectangle.Fill = image;
        }

        private void CheckListItemContent_LostFocus(object sender, RoutedEventArgs e)
        {
            IsCheckListItemGotFocus = false;
            var textBlock = sender as TextBox;
            if (textBlock == null || textBlock.Parent == null)
            {
                return;
            }
            var rectangle = (textBlock.Parent as Grid).Children.LastOrDefault(t => t is Rectangle) as Rectangle;
            if (rectangle == null || IsCheckListItemGotFocus)
            {
                return;
            }
            var image = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/Scale-100/ic_mp_move_light.png"))
            };
            rectangle.Fill = image;
        }
        /// <summary>
        /// 标记列表项是否获得焦点
        /// </summary>
        private bool IsCheckListItemGotFocus = false;// TODO  lostfocus和tapped谁先触发的问题
        /// <summary>
        /// 删除单项checklistitem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListItemDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var reactangle = sender as Rectangle;
            if (reactangle == null || !IsCheckListItemGotFocus)//symbol.Symbol != Symbol.Delete)
            {
                // TODO 之后判断是否是拖动排序操作
                return;
            }
            var checkListItem = reactangle.Tag as ChecklistItem;
            if (checkListItem == null)
            {
                return;
            }

            // TODO 之后使用INotifyPropertyChanged来进行操作
            ViewModel.TrulyCheckListItems.Remove(checkListItem);
            //this.listViewTasksContentChecklist.ItemsSource = ViewModel.TrulyCheckListItems;
        }


        private void CheckListItemCheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            //var source = e.OriginalSource;
            var rectangle = sender as Rectangle;
            if (rectangle == null)
            {
                return;
            }
            var checkListItem = rectangle.Tag as ChecklistItem;
            if (checkListItem == null)
            {
                return;
            }
            //var realCheckListItem = ViewModel.TrulyCheckListItems.Where(c => c.Id == checkListItem.Id).FirstOrDefault();
            //if (realCheckListItem == null)
            //{
            //    return;
            //}

            if (checkListItem.Checked == ModelStatusEnum.COMPLETED)
            {
                ViewModel.TrulyCheckListItems.Remove(checkListItem);
                checkListItem.Checked = ModelStatusEnum.NOT_COMPLETED;
                ViewModel.TrulyCheckListItems.Insert(0, checkListItem);
            }
            else
            {
                ViewModel.TrulyCheckListItems.Remove(checkListItem);
                checkListItem.Checked = ModelStatusEnum.COMPLETED;
                ViewModel.TrulyCheckListItems.Add(checkListItem);
            }
            //this.listViewTasksContentChecklist.ItemsSource = ViewModel.TrulyCheckListItems;

            // TODO 之后使用INotifyPropertyChanged来进行操作         
        }

        private void TasksTitle_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }
            ViewModel.ChangeTitle(textBox.Text);
        }

        private void RepeatTimeComboxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems.FirstOrDefault() as RepeatTimeSelection;
            if (selectedItem == null)
            {
                return;
            }
            ViewModel.ChangeRepeatTime(selectedItem.RepeatTimeEnum);
        }


        /// <summary>
        /// checkListItme的内容改变的时候会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListItemContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBlock = sender as TextBox;
            if (textBlock == null)
            {
                return;
            }
            var checkListItem = textBlock.Tag as ChecklistItem;
            if (checkListItem == null)
            {
                return;
            }
            checkListItem.Title = textBlock.Text;
        }
        /// <summary>
        /// 当checklistitem的内容编辑状态，获取键盘输入，进行判断操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListItemContent_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // 按下了回车键，新建一个子任务
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ViewModel.AddNewCheckListItemByEnterKeyDown();

            }
        }

        private void TasksContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            var content = this.txtTasksContent.Text;
            if (string.IsNullOrEmpty(content))
            {
                ViewModel.Tasks.Title = string.Empty;
                ViewModel.Tasks.Content = string.Empty;
                return;
            }
            if (!content.Contains("\r\n"))
            {
                ViewModel.Tasks.Title = content;
                ViewModel.Tasks.Content = string.Empty;
                return;
            }
            else
            {
                ViewModel.Tasks.Title = content.Substring(0, content.IndexOf("\r\n", 0, StringComparison.Ordinal));
                ViewModel.Tasks.Content = content.Substring(content.IndexOf("\r\n", 0, StringComparison.Ordinal));
            }
        }


    }
    /// <summary>
    /// 临时使用的类
    /// </summary>
    public class SnoozeTimeSelection
    {
        public string Name { get; set; }
        public string SnoozeValue { get; set; }
        public int SnoozeBackValue { get; set; }
    }
}
