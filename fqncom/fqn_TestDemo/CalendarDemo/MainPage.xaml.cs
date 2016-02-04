using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Contacts;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace CalendarDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ContactPicker ContactPicker = new ContactPicker();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.ContactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        //this function is going to management the calendar whick means you can defination your own calendar style
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //await AppointmentManager.ShowTimeFrameAsync(DateTimeOffset.Now, TimeSpan.FromHours(1));
            Appointment ap = new Appointment();
            ap.Subject = "this is a new appointment";
            ap.StartTime = System.DateTime.Now;
            await AppointmentManager.ShowAddAppointmentAsync(ap, new Rect());
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Contact contact = await this.ContactPicker.PickContactAsync();
            if (contact!=null)
            {
                MessageDialog messageDialog = new MessageDialog(string.Format("user's phoneNumber is {0}", contact.Phones.First().Number), contact.DisplayName);
                await messageDialog.ShowAsync();
            }
        }
    }
}
