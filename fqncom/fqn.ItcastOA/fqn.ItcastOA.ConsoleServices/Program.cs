using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace fqn.ItcastOA.ConsoleServices
{
    class Program
    {
        static void Main(string[] args)
        {
            IScheduler sched;
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();
            JobDetail job = new JobDetail("job1", "group1", typeof(IndexJob));//IndexJob为实现了IJob接口的类
            DateTime ts = TriggerUtils.GetNextGivenSecondDate(null, 5);//5秒后开始第一次运行
            TimeSpan interval = TimeSpan.FromSeconds(5);//每隔5秒执行一次
            Trigger trigger = new SimpleTrigger("trigger1", "group1", "job1", "group1", ts, null,
                                                    SimpleTrigger.RepeatIndefinitely, interval);//每若干小时运行一次，小时间隔由appsettings中的IndexIntervalHour参数指定

            sched.AddJob(job, true);
            sched.ScheduleJob(trigger);
            sched.Start();
            //要关闭任务定时则需要sched.Shutdown(true)
        }
    }
}
