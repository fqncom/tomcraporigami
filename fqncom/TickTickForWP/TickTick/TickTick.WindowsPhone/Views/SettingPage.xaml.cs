using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TickTick.Helper;
using TickTick.Models;
using TickTick.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPageViewModel ViewModel { get; set; }

        public SettingPage()
        {
            ViewModel = new SettingPageViewModel();
            this.InitializeComponent();
            //this.NavigationCacheMode = NavigationCacheMode.Required;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed_SettingPage;

            DrawerLayout.InitializeDrawerLayout();
        }

        private void HardwareButtons_BackPressed_SettingPage(object sender, BackPressedEventArgs e)
        {

            //if (this.Frame.CanGoBack)
            //{
            //    this.Frame.GoBack();
            //}
            e.Handled = true;

            HardwareButtons.BackPressed -= HardwareButtons_BackPressed_SettingPage;
            NavigateHelper.NavigateToPage(typeof(MainPage));
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.GetUserProfile();

            #region 不必重新绑定

            //this.listViewCommon.ItemsSource = ViewModel.SettingItemCommon;
            //this.listViewProfile.ItemsSource = ViewModel.SettingItemProfile;
            //this.listViewHelps.ItemsSource = ViewModel.SettingItemHelps;
            //this.listViewAbout.ItemsSource = ViewModel.SettingItemAbout;

            #endregion
        }

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
            }
            else
            {
                DrawerLayout.OpenDrawer();
            }
            e.Handled = true;
        }

        private void ListViewSettingItem_Clicked(object sender, ItemClickEventArgs e)
        {
            //获取要进行设置的项
            var settingItem = e.ClickedItem as SettingItem;

            if (settingItem == null)
            {
                //为空，则不进行任何操作
                return;
            }
            NavigateHelper.NavigateToPageWithParam(typeof(SettingDetailPage), 
                new FrameTransitionParam { UserProfile = ViewModel.UserProfile, SettingItem = settingItem });
            //NavigateHelper.NavigateToPageWithParam(typeof(BlankPage1), new FrameTransitionParam { SettingItem = settingItem });
            //var frame = Window.Current.Content as Frame;
            //this.Frame.Navigate(typeof(SettingDetailPage), new FrameTransitionParam { SettingItem = settingItem });


        }




        #region 废弃
        //private void SettingPivotSelection_Changed(object sender, SelectionChangedEventArgs e)
        //{
        //    var pivot = sender as Pivot;
        //    var selectPivotItem = pivot.SelectedItem as PivotItem;
        //    switch (selectPivotItem.Tag.ToString())
        //    {
        //        case "Common":
        //            break;
        //        case "Profile":
        //            break;
        //        case "Help":
        //            break;
        //        case "About":
        //            break;
        //        default:
        //            break;
        //    }
        //} 
        #endregion
    }
}
