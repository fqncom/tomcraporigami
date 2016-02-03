using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Notifications;
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
    public sealed partial class NotificationDemo : Page
    {
        public NotificationDemo()
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


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            //XmlDocument dom = new XmlDocument();
            //dom = Windows.UI.Notifications.ToastNotificationManager.GetTemplateContent(Windows.UI.Notifications.ToastTemplateType.ToastText02);
            //dom.GetElementsByTagName
            //ScheduledToastNotification scheduler = new ScheduledToastNotification(dom, DateTimeOffset.Now.AddSeconds(5));
            //scheduler.Id = "toast";
            //Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduler);
            //var seconds = 60;
            //var currentTime = new DateTime();

            //var dueTime = new DateTimeOffset(currentTime.AddMinutes(1));//new DateTimeOffset( + seconds * 60 * 1000);
            ////DateTimeOffset dueTime = new DateTimeOffset(currentTime, TimeSpan.FromMinutes(2));
            //var idNumber = new Random().Next(12, 20);  // Generates a unique ID number for the notification.

            //// Set up the notification text.
            //var toastXml = Windows.UI.Notifications.ToastNotificationManager.GetTemplateContent(Windows.UI.Notifications.ToastTemplateType.ToastText02);
            //var strings = toastXml.GetElementsByTagName("text");
            //strings[0].AppendChild(toastXml.CreateTextNode("This is a scheduled toast notification"));
            //strings[1].AppendChild(toastXml.CreateTextNode("Received: " + dueTime.ToLocalTime()));

            //// Create the toast notification object.
            //var toast = new Windows.UI.Notifications.ScheduledToastNotification(toastXml, dueTime);
            //toast.Id = "Toast" + idNumber;

            //// Add to the schedule.
            //Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);

            //var app = new Appointment();
            //app.Details = "ceshi ";
            //app.StartTime = DateTime.Now.AddSeconds(5);
            //app.Subject = "diyige yuehui";

            //AppointmentStore appointmentStore = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);

            //FindAppointmentsOptions options = new FindAppointmentsOptions();
            //options.MaxCount = 100;
            //options.FetchProperties.Add(AppointmentProperties.Subject);
            //options.FetchProperties.Add(AppointmentProperties.Location);
            //options.FetchProperties.Add(AppointmentProperties.Invitees);
            //options.FetchProperties.Add(AppointmentProperties.Details);
            //options.FetchProperties.Add(AppointmentProperties.StartTime);
            //options.FetchProperties.Add(AppointmentProperties.ReplyTime);
            //IReadOnlyList<Windows.ApplicationModel.Appointments.Appointment> appointments = await appointmentStore.FindAppointmentsAsync(DateTime.Now, TimeSpan.FromHours(24), options);
            //foreach (var appointment in appointments)
            //{
            //    var persistentId = appointment.RoamingId;
            //    var details = appointment.Details;//the details is empty, why
            //    var invitees = appointment.Invitees;//the invitees is also empty, why?
            //}
            //var appoint = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AppCalendarsReadWrite);
            //FindAppointmentsOptions options = new FindAppointmentsOptions();
            //options.MaxCount = 100;
            //options.FetchProperties.Add(AppointmentProperties.Subject);
            //options.FetchProperties.Add(AppointmentProperties.Location);
            //options.FetchProperties.Add(AppointmentProperties.Invitees);
            //options.FetchProperties.Add(AppointmentProperties.Details);
            //options.FetchProperties.Add(AppointmentProperties.StartTime);
            //options.FetchProperties.Add(AppointmentProperties.ReplyTime);
            
            //var app1 = new Appointment();
            //app1.AllDay = true;
            //app1.Details = "app1信息";
            //app1.Duration = TimeSpan.FromDays(5);
            //app1.StartTime = DateTime.Now.AddSeconds(5);
            
            ////app1.Recurrence = new AppointmentRecurrence()
            ////app1.Reminder//提前量
            //app1.Subject = "zhutishi ";
            ////app1.Uri = new Uri


            //var appoints = await appoint.FindAppointmentsAsync(DateTime.Now, TimeSpan.FromHours(24), options);
           
            //var newAppoint = await appoint.CreateAppointmentCalendarAsync("测试");
            
            ////newAppoint.LocalId = "sdf";
            //newAppoint.DisplayName = "ceshiDisplay";
            //newAppoint.SummaryCardView = AppointmentSummaryCardView.System;
            ////await newAppoint.SaveAppointmentAsync(app1);

            //await newAppoint.DeleteAsync();
            ////await newAppoint.SaveAsync();
            ////newAppoint.DisplayCol

                //Alarm














        }
        // 弹出ToastText01模板的Toast通知
        //private void toastText01_Click(object sender, RoutedEventArgs e)
        //{
        //    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
        //    XmlNodeList elements = toastXml.GetElementsByTagName("text");
        //    elements[0].AppendChild(toastXml.CreateTextNode("Hello Windows Phone 8.1"));
        //    ToastNotification toast = new ToastNotification(toastXml);
        //    toast.Activated += toast_Activated;
        //    toast.Dismissed += toast_Dismissed;
        //    toast.Failed += toast_Failed;
        //    toast.SuppressPopup = true;
        //    ToastNotificationManager.CreateToastNotifier().Show(toast);
        //}
        //// 弹出ToastText02模板的Toast通知
        //private void toastText02_Click(object sender, RoutedEventArgs e)
        //{
        //    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
        //    XmlNodeList elements = toastXml.GetElementsByTagName("text");
        //    elements[0].AppendChild(toastXml.CreateTextNode("WP8.1"));
        //    elements[1].AppendChild(toastXml.CreateTextNode("Hello Windows Phone 8.1"));
        //    ToastNotification toast = new ToastNotification(toastXml);
        //    toast.Activated += toast_Activated;
        //    toast.Dismissed += toast_Dismissed;
        //    toast.Failed += toast_Failed;
        //    ToastNotificationManager.CreateToastNotifier().Show(toast);
        //}
        //// 直接使用XML字符串来拼接出ToastText02模板的Toast通知
        //private void toastXML_Click(object sender, RoutedEventArgs e)
        //{
        //    string toastXmlString = "<toast>"
        //    + "<visual>"
        //    + "<binding template='ToastText02'>"
        //    + "<text id='1'>WP8.1</text>"
        //    + "<text id='2'>" + "Received: " + DateTime.Now.ToLocalTime() + "</text>"
        //    + "</binding>"
        //    + "</visual>"
        //    + "</toast>";
        //    XmlDocument toastXml = new XmlDocument();
        //    toastXml.LoadXml(toastXmlString);
        //    ToastNotification toast = new ToastNotification(toastXml);
        //    toast.Activated += toast_Activated;
        //    toast.Dismissed += toast_Dismissed;
        //    toast.Failed += toast_Failed;
        //    ToastNotificationManager.CreateToastNotifier().Show(toast);
        //}
        //// Toast通知弹出失败的事件
        //async void toast_Failed(ToastNotification sender, ToastFailedEventArgs args)
        //{
        //    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            info.Text = "Toast通知失败：" + args.ErrorCode.Message;
        //        });
        //}
        //// Toast通知消失的事件，当通知自动消失或者手动取消会触发该事件
        //async void toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        //{
        //    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            info.Text = "Toast通知消失：" + args.Reason.ToString();
        //        });
        //}
        //// Toast通知激活的事件，当通知弹出时，点击通知会触发该事件
        //async void toast_Activated(ToastNotification sender, object args)
        //{
        //    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        info.Text = "Toast通知激活";
        //    });
        //}
        //// 定时Toast通知
        //private void scheduledToast_Click(object sender, RoutedEventArgs e)
        //{
        //    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
        //    XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
        //    toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode("Toast title"));
        //    toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode("Toast content"));
        //    DateTime startTime = DateTime.Now.AddSeconds(3);
        //    ScheduledToastNotification recurringToast = new ScheduledToastNotification(toastXml, startTime);
        //    recurringToast.Id = "ScheduledToast1";
        //    ToastNotificationManager.CreateToastNotifier().AddToSchedule(recurringToast);
        //}
    }
}
