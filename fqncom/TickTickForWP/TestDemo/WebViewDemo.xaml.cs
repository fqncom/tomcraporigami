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
    public sealed partial class WebViewDemo : Page
    {
        public WebViewDemo()
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
            this.drawerLayout.InitializeDrawerLayout();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (this.drawerLayout.IsDrawerOpen)
            {
                this.drawerLayout.CloseDrawer();
                e.Handled = true;
            }
            else
            {
                this.drawerLayout.OpenDrawer();
                e.Handled = true;
                //Application.Current.Exit();
            }
        }

        public List<string> UrlList = new List<string>();

        private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtUrl.Text))
            {
                this.UrlList.Add(this.txtUrl.Text);
                this.listView.ItemsSource = this.UrlList;
            }
        }

        private void ListViewItem_Tapped(object sender, ItemClickEventArgs e)
        {
            var listViewItem = e.ClickedItem.ToString();
            if (listViewItem != null)
            {
                this.webView.Navigate(new Uri(string.Format("https://{0}", listViewItem), UriKind.RelativeOrAbsolute));
                this.webView.NavigationCompleted += (s1, e1) => { this.drawerLayout.CloseDrawer(); };

                this.drawerLayout.DrawerClosed += (s1) =>
                {

                };
            }
        }
    }
}
