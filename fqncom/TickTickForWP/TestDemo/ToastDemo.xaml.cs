using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using NotificationsExtensions.ToastContent;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ToastDemo : Page
    {
        public ToastDemo()
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
            //e.Parameter
            if (e.Parameter.ToString()==string.Empty)
            {
                return;
            }
            //DrawerLayout.InitializeDrawerLayout();

            //string[] menuItems = new string[5] { "Item1", "Item2", "Item3", "Item4", "Item5" };
            //ListMenuItems.ItemsSource = menuItems.ToList();

            //Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (DrawerLayout.IsDrawerOpen)
            //    DrawerLayout.CloseDrawer();
            //else
            //    DrawerLayout.OpenDrawer();
        }
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //}

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            //if (DrawerLayout.IsDrawerOpen)
            //{
            //    DrawerLayout.CloseDrawer();
            //    e.Handled = true;
            //}
            //else
            //{
            //    Application.Current.Exit();
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").FirstOrDefault();
            textNode.InnerText = "this is a test";
            var toastNotification = new ToastNotification(toastTmpl);
            var toast = new ScheduledToastNotification(toastTmpl,DateTimeOffset.Now.AddSeconds(5));

            toastNotification.SuppressPopup = false;
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            //if (toastTmpl == null)
            //{
            //    System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
            //    return;
            //}
            //var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            //textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            //textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            //var toastNotification = new ToastNotification(toastTmpl);

            //toastNotification.SuppressPopup = false;

            //ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            //IToastImageAndText01 toast = ToastContentFactory.CreateToastImageAndText01();
            
            IToastNotificationContent toast= ToastContentFactory.CreateToastImageAndText01();
            toast.Launch = "#/UIDemo.xaml";
            toast.Duration = ToastDuration.Long;
            //toast.Lang = "zh-cn";
            //toast.BaseUri = "ms-appx:///Assets";
            //toast.AddImageQuery = 
            //toast.Audio.Content = ToastAudioContent.Reminder;
            toast.Audio.Content = ToastAudioContent.LoopingAlarm;
            toast.Audio.Loop = true;
            //toast.IncomingCallCommands.ShowDeclineCommand = true;
            //toast.IncomingCallCommands.DeclineArgument = "DeclineArgument";
            //toast.IncomingCallCommands.ShowVideoCommand = true;
            //toast.IncomingCallCommands.VideoArgument = "VideoArgument";
            //toast.IncomingCallCommands.ShowVoiceCommand = true;
            //toast.IncomingCallCommands.VoiceArgument = "VoiceArgument";

            toast.AlarmCommands.ShowDismissCommand = true;
            toast.AlarmCommands.ShowSnoozeCommand = true;
            toast.AlarmCommands.SnoozeArgument = "SnoozeArgument";
            toast.AlarmCommands.DismissArgument = "DismissArgument";

            var str = toast.GetContent();
            string toastXmlString =
                    "<toast duration=\"long\">\n" +
                        "<visual>\n" +
                            "<binding template=\"ToastText02\">\n" +
                                "<text id=\"1\">Alarms Notifications SDK Sample App</text>\n" +
                                "<text id=\"2\">" + "test" + "</text>\n" +
                            "</binding>\n" +
                        "</visual>\n" +
                        "<commands scenario=\"alarm\">\n" +
                            "<command id=\"snooze\"/>\n" +
                            "<command id=\"dismiss\"/>\n" +
                        "</commands>\n" +
                    "</toast>\n";

            // Display the generated XML for demonstration purposes.
            //rootPage.NotifyUser(toastXmlString, NotifyType.StatusMessage);

            // Create an XML document from the XML.
            var toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
            toastDOM.LoadXml(toastXmlString);
            //var xml = toast.GetXml();
            //toast.CreateNotification();

            // windows phone 不支持自定义alarm提醒 ：https://msdn.microsoft.com/en-us/library/dn642486(v=vs.105).aspx
            var toastNotification = new ScheduledToastNotification(toastDOM, DateTimeOffset.Now.AddSeconds(5),TimeSpan.FromSeconds(61),1);
            //toastNotification.Tag
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toastNotification);
            //ToastNotificationManager.History
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastContentFactory.CreateToastImageAndText03();
            toastTmpl.BaseUri = "ms-appx:///";
            toastTmpl.Image.Src = "Assets/Images/avatar.jpg";
            toastTmpl.Image.Alt = "logo";
            toastTmpl.TextHeadingWrap.Text = "this is a title";
            toastTmpl.TextBody.Text = "this is content";
            toastTmpl.Audio.Content = ToastAudioContent.IM;
            toastTmpl.Audio.Loop = false;
            toastTmpl.Duration = ToastDuration.Long;
            toastTmpl.Launch = string.Format("/toastdemo.xaml?param={0}", "ok");
            toastTmpl.StrictValidation = true;
            ScheduledToastNotification toast = new ScheduledToastNotification(toastTmpl.GetXml(), DateTimeOffset.Now.AddSeconds(5),TimeSpan.FromSeconds(60),3);
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);

            //var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText03);
            //if (toastTmpl == null)
            //{
            //    System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
            //    return;
            //}
            //var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            //textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            //textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            //var toastNotification = new ToastNotification(toastTmpl);

            //toastNotification.SuppressPopup = false;

            //ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText04);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            textNode[2].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            var toastNotification = new ToastNotification(toastTmpl);

            toastNotification.SuppressPopup = false;

            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").FirstOrDefault();
            var imgNode = toastTmpl.GetElementsByTagName("image").FirstOrDefault();

            textNode.InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            //imgNo = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            var toastNotification = new ToastNotification(toastTmpl);

            toastNotification.SuppressPopup = false;

            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            textNode[2].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            var toastNotification = new ToastNotification(toastTmpl);

            toastNotification.SuppressPopup = false;

            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText03);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            textNode[2].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            var toastNotification = new ToastNotification(toastTmpl);

            toastNotification.SuppressPopup = false;

            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
            if (toastTmpl == null)
            {
                System.Diagnostics.Debug.WriteLine("toastTmpl等于null");
                return;
            }
            var textNode = toastTmpl.GetElementsByTagName("text").ToList();

            textNode[0].InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";
            textNode[1].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            textNode[2].InnerText = "this is a testthis is a testthis is a testthis is a testthis is a testthis is a test";
            var toastNotification = new ToastNotification(toastTmpl);

            toastNotification.SuppressPopup = false;

            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //ToastPrompt toast = new ToastPrompt();
            //toast.Title = "this is a prompt toast";
            //toast.Message = "Some message";
            //toast.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/drawer_icon.png", UriKind.RelativeOrAbsolute));

            //toast.Completed += toast_Completed;
            //toast.Show();

            string toastXmlString =
                    "<toast duration=\"long\">\n" +
                        "<visual>\n" +
                            "<binding template=\"ToastText02\">\n" +
                                "<text id=\"1\">Alarms Notifications SDK Sample App</text>\n" +
                                "<text id=\"2\">" + "ceshi alarm" + "</text>\n" +
                            "</binding>\n" +
                        "</visual>\n" +
                        "<commands scenario=\"alarm\">\n" +
                            "<command id=\"snooze\"/>\n" +
                            "<command id=\"dismiss\"/>\n" +
                        "</commands>\n" +
                        "<audio src=\"ms-winsoundevent:Notification.Looping.Alarm2\" loop=\"true\" />\n" +
                    "</toast>\n";

            
            var toastTmpl = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var textNode = toastTmpl.GetElementsByTagName("text").FirstOrDefault();
            textNode.InnerText = "this is a headerthis is a headerthis is a headerthis is a headerthis is a header";


            IXmlNode toastNodeRoot = toastTmpl.SelectSingleNode("/toast");

            XmlElement audioNode = toastTmpl.CreateElement("audio");
            audioNode.SetAttribute("src", "ms-winsoundevent:Notification.IM");


            //添加按钮
            //XmlElement commandsNode = toastTmpl.CreateElement("commands");
            //commandsNode.SetAttribute("scenario", "alarm");

            //IXmlNode commandsNodeRoot = toastTmpl.SelectSingleNode("/commands");
            //XmlElement commandNode1 = toastTmpl.CreateElement("command");
            //commandNode1.SetAttribute("id", "snooze");
            //XmlElement commandNode2 = toastTmpl.CreateElement("command");
            //commandNode2.SetAttribute("id", "dismiss");
            //commandsNode.AppendChild(commandNode1);
            //commandsNode.AppendChild(commandNode2);

            //toastNodeRoot.AppendChild(commandsNode);
            toastNodeRoot.AppendChild(audioNode);


            ScheduledToastNotification toast = new ScheduledToastNotification(toastTmpl, DateTime.UtcNow.AddSeconds(5));
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }

        //private void toast_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        //{

        //}
    }
}
