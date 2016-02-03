using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
    public sealed partial class FontDemo : Page
    {
        public FontDemo()
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
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            //Windows.UI.Popups.PopupMenu menu = new Windows.UI.Popups.PopupMenu();
            //await menu.ShowAsync(new Point(20,30));
            //this.pop.IsOpen = true;
            //System.Diagnostics.Debug.WriteLine(scaleFactor);
            this.txtMsg.Text = scaleFactor.ToString();
            this.txtWidth.Text = (Window.Current.Bounds.Width * scaleFactor).ToString();
            this.txtHeight.Text = (Window.Current.Bounds.Height * scaleFactor).ToString();

            ResourceMap resources = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
            List<string> strList = new List<string>();
            foreach (var item in resources)
            {
                strList.Add(item.Key);
            }
            foreach (var item in strList)
            {
                string str = resources.GetValue(item, new ResourceContext()).ValueAsString;
                float num = 0;
                if (str.IsNumeric(num))
                {
                    
                }
            }

            foreach (var item in resources)
            {
                //float oldNum = 0;
                //if (.IsNumeric(oldNum))
                //{
                //    dic[item.Key] = (oldNum * 1.6).ToString();
                //}
            }

            //var context =("Resources");
            //var current = ResourceLoader.GetForCurrentView();
            //var context = ResourceContext.GetForCurrentView();
            //var dic = context.;

            //foreach (var item in dic)
            //{
            //    float oldNum = 0;
            //    if (item.Value.IsNumeric(oldNum))
            //    {
            //        dic[item.Key] = (oldNum * 1.6).ToString();
            //    }
            //}
            //var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            //var str = loader.GetString("Farewell");
            //var resourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext();

            //// Set the specific context for lookup of resources.
            //var qualifierValues = resourceContext.QualifierValues;
            //foreach (var item in qualifierValues)
            //{

            //}

        }
    }
}
