using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace DataBindingDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.Data = new MainPageData { Text1 = "this is textbox1", Text2 = "this is textbox2" };

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public MainPageData Data { get; set; }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            if (this.slider==null)
            {
                return;
            }
            double d = this.slider.Value;
            //if (this.ellipse != null)
            //{
            //    var by = Convert.ToByte(slider.Value >= 255 ? 255 : slider.Value);
            //    this.ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, by));
            //}
        }
    }

    public class ValueToColorConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = value as ColorRgbByte;

            if (color == null)
            {
                color.Opacity = 200;
                color.RedByte = 69;
                color.GreenByte = 186;
                return null;
            }
            return new SolidColorBrush(Color.FromArgb(color.Opacity, color.RedByte, color.GreenByte, color.BlueByte));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        #endregion
    }

    public class ColorRgbByte
    {
        public byte Opacity { get; set; }
        public byte RedByte { get; set; }
        public byte GreenByte { get; set; }
        public byte BlueByte { get; set; }
    }

    public class MainPageData
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
    }
}
