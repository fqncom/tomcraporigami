using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace AccelerometerDemo
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Accelerometer ac = Accelerometer.GetDefault();
            ac.Shaken += ac_Shaken;
            ac.ReportInterval = 200;
            Window.Current.VisibilityChanged += (p1, p2) =>
            {
                if (p2.Visible)
                {
                    ac.ReadingChanged += ac_ReadingChanged;
                }
                else
                {
                    ac.ReadingChanged -= ac_ReadingChanged;
                }
            };
        }

        async void ac_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            double x = args.Reading.AccelerationX * 100d;
            double y = args.Reading.AccelerationY * 100d;
            double z = args.Reading.AccelerationZ * 100d;
            System.Diagnostics.Debug.WriteLine("X={0:NO},Y={1:NO},Z={2}", x, y, z);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                if (Math.Abs(x) > 100d || Math.Abs(y) > 60d || Math.Abs(z) > 50d)
                {
                    try
                    {
                        int imageIndex = new Random().Next(1, 5);
                        //this codes is going to read the image file from the storage that in the phone rather than in the computer.
                        Windows.Storage.StorageFile theFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(string.Format("ms-appx:///Assets/f{0}.png", imageIndex), UriKind.Absolute));
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.SetSource(await theFile.OpenReadAsync());
                        this.img.Source= bitmap;
                    }
                    catch (Exception e)
                    {
                        
                        throw e;
                    }
                }
            });
        }

        void ac_Shaken(Accelerometer sender, AccelerometerShakenEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
