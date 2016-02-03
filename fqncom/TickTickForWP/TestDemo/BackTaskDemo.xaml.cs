using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BackTaskDemo : Page
    {
        public BackTaskDemo()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == "Reminder")
                {
                    task.Value.Progress += Value_Progress;
                    task.Value.Completed += Value_Completed;
                    UpdateUI("后台任务已经存在", "");
                }
            }
        }

        private async void UpdateUI(string p1, string p2)//更新UI
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    progressText.Text = p1;
                    statusText.Text = p2;
                });
        }
        //处理后台任务完成事件
        void Value_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            UpdateUI("100%", "后台任务完成!");
        }
        //处理后台任务进度
        void Value_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            var progress = args.Progress + "%";
            UpdateUI("后台任务进行中:", progress);
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            TimeTrigger timetrigger = new TimeTrigger(30, false);
            PushNotificationTrigger notificationTrigger = new PushNotificationTrigger();
            


            var SampleTask = new BackgroundTaskBuilder(); //创建后台任务实例
            SampleTask.Name = "Reminder";  //指定后台任务名称
            SampleTask.TaskEntryPoint = "ReminderComponent.Reminder";//指定后台任务名称
            SampleTask.SetTrigger(timetrigger);//指定后台任务的触发器

            SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
            SampleTask.AddCondition(internetCondition);

            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity)
            {
                BackgroundTaskRegistration task = SampleTask.Register();
                task.Progress += Value_Progress; ;
                task.Completed += Value_Completed; ;
                UpdateUI("", "注册成功");

                registerButton.IsEnabled = false;
                cancelButtton.IsEnabled = true;

                var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                settings.Values.Remove(task.Name);
            }
            else if (access == BackgroundAccessStatus.Denied)//用户禁用后台任务或后台任务数量已达最大
            {
                await new MessageDialog("您已禁用后台任务或后台任务数量已达最大!").ShowAsync();
            }

        }

        private void cancelButtton_Click(object sender, RoutedEventArgs e)//删除后台任务
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == "Reminder")
                {
                    task.Value.Unregister(true);//删除后台任务
                }
            }
            registerButton.IsEnabled = true;
            cancelButtton.IsEnabled = false;
            UpdateUI("", "后台任务取消");
        }
    }

}
