using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TickTick.Utilities;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using WinRTXamlToolkit.Controls.Extensions;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UIDemo : Page
    {
        public UIDemo()
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
            
            //this.listview.ItemsSource = new ObservableCollection<City> { new City("hehe ", 15), new City("hehe ", 15), new City("hehe ", 15), new City("hehe ", 15) };
            //this.calendar.DisplayDate = DateTime.Now;
            var scaleEnum = DisplayInformation.GetForCurrentView().ResolutionScale;
            ////var imageList = this.stack.GetChildren() ;
            ////foreach (Image item in imageList)
            ////{
            ////    item.Height = 
            ////}
            double scale = 1;
            switch (scaleEnum)
            {

                case ResolutionScale.Scale100Percent:
                    scale = 1;
                    break;
                case ResolutionScale.Scale140Percent:
                    scale = 1.4;
                    break;
                case ResolutionScale.Scale180Percent:
                    scale = 1.8;
                    break;
            }
            //this.ImageTemp.Source.SetValue(WidthProperty, this.Image.ActualHeight * scale)
            //;
            var bitmap = this.ImageTest.Source as BitmapImage;
            var name = bitmap.UriSource.OriginalString;
            this.text.Text = string.Format("当前分辨率:{0}\r\n,缩放比{1}\r\n,actualWidth:{2}\r\n,actualHeight{3}\r\n,boundsWidth{4}\r\n,boundsHeight{5}\r\n,当前最后一张图的文件名：{6}", scaleEnum.ToString(), scale, this.ActualWidth, this.ActualHeight, Window.Current.Bounds.Width, Window.Current.Bounds.Height,name);

        }

        private void ListView_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(e.OriginalSource as FrameworkElement);
        }

        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            //foreach (var item in e.Items)
            //{
            //    // If you want to drop only a subset of the dragged items,
            //    // then make each key unique. 
            //    e.Data.Properties.Add("MyApp.MyCity", item);
            //}
            //DragDropEffects
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            //DataPackageView dpView = e.Data.GetView();
            //foreach (var prop in dpView.Properties)
            //{
            //    var city = prop.Value as City;
            //    this.listview.Items.Add(city);
            //}
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.popUpChangePriorities.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //this.popUpChangePriorities.IsOpen = true;
            //var element = this.grid as FrameworkElement;
            //var listFlyout = FlyoutBase.GetAttachedFlyout(element);
            //listFlyout.ShowAt(element);
            var datetime = DateTime.UtcNow;
            //this.text.Text = datetime.ToLocalTime().ToString("dd/MM");
            //this.text.Text = datetime.ToLocalTime().ToString("HH:mm");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.logincontrol1.IsOpen = true;
        }

        private void logincontrol1_Loaded(object sender, RoutedEventArgs e)
        {
            //this.gridToastTasksDetail.Width = Window.Current.Bounds.Width;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //TickRRule rule = new TickRRule();
            //TickRRule rrule = new TickRRule("");
            DDay.iCal.RecurrencePattern pattern = new DDay.iCal.RecurrencePattern();
        }

        private void Icon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rect = sender as Rectangle;
            var image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/btn_check_on_holo_dark.png"));
            rect.Fill = image;
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var resolutionScale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            //this.TxtResolutionScale.Text = resolutionScale.ToString();

        }

        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle.IsOn)
            {
                this.RequestedTheme = ElementTheme.Light;
            }
            else
            {
                this.RequestedTheme = ElementTheme.Dark;
            }
        }
    }
    public sealed class City
    {
        public City(String name, int pop)
        {
            this.Name = name;
            this.Population = pop;
        }
        public String Name { get; set; }
        public int Population { get; set; }
    }

}
