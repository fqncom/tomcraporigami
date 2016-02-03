using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Models;
using TickTick.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TickTick.Enums;
using TickTick.Enum;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TasksDetailPage : Page
    {
        #region 自定义属性
        /// <summary>
        /// 导航过来是为了更新还是新增，true标识更新，false表示新增
        /// </summary>
        public bool IsNavigateForUpdate { get; set; }
        public TasksDetailPageViewModel ViewModel { get; set; }
        #endregion

        public TasksDetailPage()
        {
            ViewModel = new TasksDetailPageViewModel();
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //获取传输过来的projects
            var param = e.Parameter as FrameTransitionParam;
            if (param == null)
            {
                return;
            }
            if (param.Projects == null)
            {
                //if (LoggerHelper.IS_LOG_ENABLED)
                //{
                //    LoggerHelper.LogToAllChannels(null, string.Format("页面间传递的参数有问题：{0}", JsonConvert.SerializeObject(param)));
                //}
                return;
            }
            //拿到projects的Id
            ViewModel.Projects = param.Projects;
            //如果存在tasks表示当前是更新，如果不存在tasks，表示当前是新增
            if (param.Tasks != null)
            {
                ViewModel.OriginalTasks = param.Tasks;
                ViewModel.Tasks = ObjectCopier.Clone<Tasks>(param.Tasks);
                this.IsNavigateForUpdate = true;
                //this.txtDueDate.Text = ViewModel.Tasks.DueDate.ToString("");
            }
            else
            {
                this.IsNavigateForUpdate = false;
            }

            #region 测试
            this.txtDueDate.AddHandler(TappedEvent, new TappedEventHandler(TxtDueDate_Tapped), true);
            this.txtDueDateTime.AddHandler(TappedEvent, new TappedEventHandler(TxtDueDateTime_Tapped), true);
            this.txtRemind.AddHandler(TappedEvent, new TappedEventHandler(TxtRemind_Tapped), true);
            this.txtRepeat.AddHandler(TappedEvent, new TappedEventHandler(TxtRepeat_Tapped), true);


            //this.listViewPriorities.ItemsSource = new List<PrioritiesEnum> 
            //{ 
            //    PrioritiesEnum.HighPriorities, 
            //    PrioritiesEnum.MiddlePriorities, 
            //    PrioritiesEnum.LowPriorities, 
            //    PrioritiesEnum.NonePriorities 
            //};
            this.listPickerFlyoutRemind.ItemsSource = new List<SnoozeTimeSelection> 
            { 
                new SnoozeTimeSelection { Name = "no reminder", SnoozeValue = MinuteIncrementEnum.No_Reminder } ,
                new SnoozeTimeSelection { Name = "at start time", SnoozeValue = MinuteIncrementEnum.At_StartTime } ,
                new SnoozeTimeSelection { Name = "5 minutes", SnoozeValue = MinuteIncrementEnum.Five_Minute} ,
                //new RemindTimeSelection { Name = "10 minutes", RemindValue = MinuteIncrementEnum.Ten_Minute } ,
                //new RemindTimeSelection { Name = "15 minutes", RemindValue = MinuteIncrementEnum.Fifteen_Minute } ,
                new SnoozeTimeSelection { Name = "30 minutes", SnoozeValue = MinuteIncrementEnum.Half_An_Hour } ,
                new SnoozeTimeSelection { Name = "1 hour", SnoozeValue = MinuteIncrementEnum.One_Hour } ,
                //new RemindTimeSelection { Name = "4 hours", RemindValue = MinuteIncrementEnum.Four_Hours } ,
                //new RemindTimeSelection { Name = "18 hours", RemindValue = MinuteIncrementEnum.No_Reminder } ,
                new SnoozeTimeSelection { Name = "1 day", SnoozeValue = MinuteIncrementEnum.No_Reminder } 
            };
            #endregion
        }

        #region 弃用

        //private async void AddOrUpdateBack_Clicked(object sender, TappedRoutedEventArgs e)
        //{

        //    //此处最好进行一个判断，是更新还是新增操作，然后进行相关的数据库操作
        //    if (this.IsNavigateForUpdate)//true表示更新
        //    {
        //        ViewModel.Tasks.Content = this.txtContent.Text;
        //        await ViewModel.UpdateTasks();
        //    }
        //    else//false 表示新增
        //    {
        //        var tasks = new Tasks { };

        //    }
        //    var frame = Window.Current.Content as Frame;
        //    frame.Navigate(typeof(MainPage));
        //} 
        #endregion

        /// <summary>
        /// 测试用返回代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TestBack_Clicked(object sender, RoutedEventArgs e)
        {
            //此处最好进行一个判断，是更新还是新增操作，然后进行相关的数据库操作
            if (this.IsNavigateForUpdate)//true表示更新
            {
                // 修改的数据
                ViewModel.Tasks.Content = this.txtContent.Text;
                await ViewModel.UpdateTasks();
            }
            else//false 表示新增
            {
                ViewModel.Tasks = new Tasks();
                ViewModel.Tasks.Title = this.txtContent.Text;
                await ViewModel.AddTasks();
            }
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// 截止日期文本框单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TxtDueDate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elementValue = sender as TextBox;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue) as DatePickerFlyout;
            flyoutBase.Date = string.IsNullOrEmpty(elementValue.Text) ? DateTime.Now.Date : Convert.ToDateTime(elementValue.Text);
            flyoutBase.YearVisible = false;
            await flyoutBase.ShowAtAsync(elementValue);
        }
        /// <summary>
        /// 时间文本框单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TxtDueDateTime_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elementValue = sender as TextBox;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue) as TimePickerFlyout;
            flyoutBase.Time = string.IsNullOrEmpty(elementValue.Text) ? DateTime.Now.TimeOfDay : Convert.ToDateTime(elementValue.Text).TimeOfDay;
            //flyoutBase.MinuteIncrement = Convert.ToInt32(MinuteIncrementEnum.One_Minute);
            await flyoutBase.ShowAtAsync(elementValue);
        }
        private async void TxtRemind_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elementValue = sender as TextBox;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue) as ListPickerFlyout;
            await flyoutBase.ShowAtAsync(elementValue);
        }
        private async void TxtRepeat_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elementValue = sender as TextBox;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue) as ListPickerFlyout;
            await flyoutBase.ShowAtAsync(elementValue);
        }

        /// <summary>
        /// 点击任务删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarBtnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            //this.popUpConfirmDelete.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //MessageDialog messageDialog = new MessageDialog("确认要删除吗？", "删除该任务");
            //messageDialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;

            var confirm = new UICommand("确认");
            confirm.Invoked = async (c) =>
            {
                await ViewModel.DeleteTasks();
                var frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(MainPage));
            };
            var cancel = new UICommand("取消");
            cancel.Invoked = (c) => { return; };

            await MessageDialogHelper.MessageDialogShowAsync("确认要删除吗？", "删除该任务", cancel, confirm);
            //messageDialog.Commands.Add(cancel);
            //messageDialog.Commands.Add(confirm);

            //await messageDialog.ShowAsync();

        }
        /// <summary>
        /// 设置优先级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarBtnPriorities_Clicked(object sender, RoutedEventArgs e)
        {
            await this.listViewPriorities.ShowAtAsync(this.gridMain);
            //var elementValue = this.gridMain as FrameworkElement;
            //var flyoutBase = FlyoutBase.GetAttachedFlyout(elementValue);
            //flyoutBase.ShowAt(gridMain);
        }
        /// <summary>
        /// 选择优先级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewPriorities_Clicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            //var prioritiesItem = (PrioritiesEnum)args.AddedItems.FirstOrDefault();
            var prioritiesItem = 12;
            var prioritiesValue = 12;
            //var prioritiesValue = PrioritiesEnum.MiddlePriorities;

            #region 这里先这样写，之后由于listItem必然会改，所以留着switch
            switch (prioritiesItem)
            {
                //case PrioritiesEnum.HighPriorities:
                //    prioritiesValue = PrioritiesEnum.HighPriorities;
                //    break;
                //case PrioritiesEnum.MiddlePriorities:
                //    prioritiesValue = PrioritiesEnum.MiddlePriorities;
                //    break;
                //case PrioritiesEnum.LowPriorities:
                //    prioritiesValue = PrioritiesEnum.LowPriorities;
                //    break;
                //case PrioritiesEnum.NonePriorities:
                //    prioritiesValue = PrioritiesEnum.NonePriorities;
                //    break;
                //default:
                //    prioritiesValue = PrioritiesEnum.MiddlePriorities;
                //    break;
            }
            #endregion

            // TODO 在按下返回键的时候会保存更改，所以这里可以不用保存？
            ViewModel.Tasks.Priority = Convert.ToInt32(prioritiesValue);
            //this.listViewPriorities.Hide();
        }
        /// <summary>
        /// 截止日期选择器选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void DueDatePicker_Picked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            var newDate = args.NewDate;
            if (newDate < DateTime.Now)
            {
                //最好提示用户时间早于当前
                await MessageDialogHelper.MessageDialogShowAsync("日期不得小于当前日期", "截止日期");
                return;
            }
            //ViewModel.Tasks.DueDate.Value.AddMilliseconds = newDate.Date;
            this.txtDueDate.Text = newDate.Date.ToStringDateMDY();
            sender.Hide();
        }

        private async void DueDateTimePicker_Picked(TimePickerFlyout sender, TimePickedEventArgs args)
        {
            //var oldDate = args.OldDate;
            var newDate = args.NewTime;
            if (Convert.ToDateTime(this.txtDueDate.Text) == DateTime.Now.Date && newDate < DateTime.Now.TimeOfDay)
            {
                //最好提示用户时间早于当前
                await MessageDialogHelper.MessageDialogShowAsync("时间不得小于当前日期", "截止日期");
                return;
            }
            //ViewModel.Tasks.DueDate.Value.AddMilliseconds(newDate.TotalMilliseconds);
            this.txtDueDateTime.Text = newDate.ToString();
            sender.Hide();
        }
        /// <summary>
        /// 当时间文本发生改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDueDate_Changed(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDueDateTime.Text))
            {
                this.txtDueDateTime.Text = DateTime.Now.ToStringDateTimeHms();//.ToString("HH:mm:ss");
            }
            UpdateDueDate();
        }
        private void TxtDueDateTime_Changed(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDueDate.Text))
            {
                this.txtDueDate.Text = DateTime.Now.ToStringDateMDY();//.ToString("MM/dd/yyyy");
            }
            UpdateDueDate();
        }

        private void UpdateDueDate()
        {
            var dueDate = string.IsNullOrEmpty(this.txtDueDate.Text) ? DateTime.Now.ToStringDateMDY() : this.txtDueDate.Text;
            var dueDateTime = string.IsNullOrEmpty(this.txtDueDateTime.Text) ? DateTime.Now.ToStringDateTimeHms() : this.txtDueDateTime.Text;

            //2015-03-29T16:00:00.000+0000
            //strDate = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"); // 2007-07-21T15:11:19.1250000+05:30   
            ViewModel.Tasks.DueDate = Convert.ToDateTime(string.Format("{0}T{1}.000+0000", Convert.ToDateTime(dueDate).ToStringDateyMd(), dueDateTime));
        }
        /// <summary>
        /// 提醒选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RemindPicker_Picked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            var selectedItem = args.AddedItems.FirstOrDefault() as SnoozeTimeSelection;
            this.txtRemind.Text = selectedItem.Name;

            switch (selectedItem.SnoozeValue)
            {
                //case MinuteIncrementEnum.No_Reminder:
                //    break;
                //case MinuteIncrementEnum.At_StartTime:
                //    break;
                //case MinuteIncrementEnum.One_Minute:
                //    break;
                //case MinuteIncrementEnum.Five_Minute:
                //    break;
                //case MinuteIncrementEnum.Ten_Minute:
                //    break;
                //case MinuteIncrementEnum.Fifteen_Minute:
                //    break;
                //case MinuteIncrementEnum.Half_An_Hour:
                //    break;
                //case MinuteIncrementEnum.One_Hour:
                //    break;
                //case MinuteIncrementEnum.Four_Hours:
                //    break;
                //case MinuteIncrementEnum.One_Day:
                //    break;
                //default:
                //    break;
            }
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
        /// <summary>
        /// 移动列表选择项单击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MovePicker_Picked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            var selectedItem = args.AddedItems.FirstOrDefault() as Projects;
            ViewModel.Tasks.ProjectId = selectedItem.Id.ToString();

            this.listPickerFlyoutMoveToProjects.Hide();
        }

    }
    /// <summary>
    /// 临时使用的类
    /// </summary>
    public class SnoozeTimeSelection
    {
        public string Name { get; set; }
        public string SnoozeValue { get; set; }
    }
}
