using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;

namespace ReminderComponent
{
    public sealed class Reminder : IBackgroundTask
    {
        bool cancelRequested = false;// 用来表示是否已经请求取消后台任务
        BackgroundTaskDeferral deferral = null;//后台任务的延时
        ThreadPoolTimer periodicTimer = null;//计时器
        uint progress = 0; //用于保存后台任务的进度
        IBackgroundTaskInstance bTaskInstance = null;//提供对后台实例的访问

        public void Run(IBackgroundTaskInstance taskInstance)//后台任务入口
        {
            //SystemTriggerType.UserPresent

            taskInstance.Canceled += taskInstance_Canceled;
            deferral = taskInstance.GetDeferral();
            bTaskInstance = taskInstance;
            periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromMinutes(5));

        }

        private void PeriodicTimerCallback(ThreadPoolTimer timer)
        {
            if ((cancelRequested == false) && (progress < 100))
            {
                progress += 10;
                bTaskInstance.Progress = progress;
            }
            else
            {
                periodicTimer.Cancel();
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var key = bTaskInstance.Task.Name;
                settings.Values[key] = (progress < 100) ? "Canceled" : "Completed";
                deferral.Complete();
            }
        }

        void taskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            cancelRequested = true;
        }
    }
}
