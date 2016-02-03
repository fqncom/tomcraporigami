using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class DataDemo : Page
    {
        public DataDemo()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var datetime = DateTime.UtcNow;
            this.txtNow.Text = datetime.ToString();
            var milliSeconds = datetime.GetAllMilliSeconds();
            this.txtDate.Text = milliSeconds.GetDateTimeByMilliSeconds().ToString();
            this.txtMilliSeconds.Text = milliSeconds.ToString();
            this.txtDatetimeConvert.Text = datetime.ToString("r");
            this.txtDatetimeConvert2.Text = datetime.ToString("R");
            this.txtDatetimeConvert3.Text = datetime.ToString("yyyy-MM-dd'T'HH:mm:ss.'GMT'");
        }
    }

    public static class Utils
    {
        public static long GetAllMilliSeconds(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }
        public static DateTime GetDateTimeByMilliSeconds(this long milliSeconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliSeconds);
        }
    }
}
