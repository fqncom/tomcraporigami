using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using TickTick.Bll;
using TickTick.Enums;
using TickTick.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingDetailPage : Page
    {
        public SettingDetailPageViewModel ViewModel { get; set; }
        public UserProfileBll UserProfileBll = new UserProfileBll();

        public SettingDetailPage()
        {
            this.ViewModel = new SettingDetailPageViewModel();
            this.InitializeComponent();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed_DetailSetting;
        }

        async void HardwareButtons_BackPressed_DetailSetting(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            //await ViewModel.SaveProfileChanged();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed_DetailSetting;
            NavigateHelper.NavigateToBackPage();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameTransitionParam;
            if (param == null)
            {
                return;
            }
            var settingItem = param.SettingItem as SettingItem;
            if (settingItem == null)
            {
                //返回上一页
                return;
            }
            switch (settingItem.SettingItemType)
            {
                case TickTick.Enums.SettingItemTypeEnum.IntelligentSetting:
                    var userProfile = param.UserProfile;
                    if (userProfile == null)
                    {
                        userProfile = await UserProfileBll.GetLastOneUserProfileInfoByUserId(App.SignUserInfo.Sid);
                    }
                    ViewModel.UserProfile = userProfile;

                    InitialToggleIsOnStatus();

                    this.gridIntelligentSetting.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                case TickTick.Enums.SettingItemTypeEnum.EmailSetting:
                    this.gridEmailSetting.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                case TickTick.Enums.SettingItemTypeEnum.ModifiedEmail:
                    this.gridModifiedEmail.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                case TickTick.Enums.SettingItemTypeEnum.ModifiedPasword:
                    this.gridModifiedPassword.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                //case TickTick.Enums.SettingItemTypeEnum.HelpSetting:
                //    this.gridIntelligentSetting.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //    break;
                //case TickTick.Enums.SettingItemTypeEnum.AboutSetting:
                //    this.gridIntelligentSetting.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //    break;
                default:
                    break;
            }
            ViewModel.SettingItem = settingItem;
        }

        private bool IsFirstComing = true;
        private void InitialToggleIsOnStatus()
        {
            var profile = ViewModel.UserProfile;

            this.toggleAll.IsOn = profile.IsShowAllList;
            this.toggle7Days.IsOn = profile.IsShow7DaysList == ModelStatusEnum.YES;
            this.toggleCompleted.IsOn = profile.IsShowCompletedList == ModelStatusEnum.YES;
            this.toggleToday.IsOn = profile.IsShowTodayList == ModelStatusEnum.YES;
            //profile.IsShowScheduledList
            //profile.IsShowTagsList
            //profile.IsShowTrashList
            IsFirstComing = false;
        }

        private async void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (IsFirstComing)
            {
                return;
            }

            var toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch == null)
            {
                return;
            }
            await ViewModel.ChangeTasksListShowContent(toggleSwitch.Tag.ToString(), toggleSwitch.IsOn);
            //await ViewModel.SaveProfileChanged();
        }

    }
}
