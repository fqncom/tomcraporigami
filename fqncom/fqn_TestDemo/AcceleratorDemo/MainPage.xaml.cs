using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace AcceleratorDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 先拿到传感器对象
            Accelerometer a = Accelerometer.GetDefault();
            if (a == null)
            {
                // 代表没有加速计传感器对象
                System.Diagnostics.Debug.WriteLine("没有加速计传感器");
                return;
            }
            a.ReadingChanged += a_ReadingChanged;
            a.ReportInterval = a.MinimumReportInterval * 5;
        }

        async void a_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(args + "改变了。。。");
            // 拿到变化值
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {

            });
        }
    }
}
