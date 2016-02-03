using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Models;
using Windows.UI.Xaml.Controls;
using TickTick.Bll;

namespace TickTick.ViewModels
{
    public class SettingPageViewModel : INotifyPropertyChanged
    {

        private UserProfileBll UserProfileBll = new UserProfileBll();


        private UserProfile _userProfile;
        /// <summary>
        /// 用户设置信息
        /// </summary>
        public UserProfile UserProfile
        {
            get { return _userProfile; }
            set
            {
                if (_userProfile != value)
                {
                    _userProfile = value;
                    OnPropertyChanged("UserProfile");
                }
            }
        }

        /// <summary>
        /// 设置选项细节项目
        /// </summary>
        public ObservableCollection<ToggleSwitch> ToggleSwitchItems { get; set; }

        #region TODO 后期将会抽象的数据
        /// <summary>
        /// 设置选项--通用
        /// </summary>
        public ObservableCollection<SettingItem> SettingItemCommon { get; set; }
        /// <summary>
        /// 设置选项--账户
        /// </summary>
        public ObservableCollection<SettingItem> SettingItemProfile { get; set; }
        /// <summary>
        /// 设置选项--帮助
        /// </summary>
        public ObservableCollection<SettingItem> SettingItemHelps { get; set; }
        /// <summary>
        /// 设置选项--关于
        /// </summary>
        public ObservableCollection<SettingItem> SettingItemAbout { get; set; }

        #endregion
        public SettingPageViewModel()
        {
            this.UserProfile = new UserProfile();

            this.ToggleSwitchItems = new ObservableCollection<ToggleSwitch>();
            this.SettingItemCommon = new ObservableCollection<SettingItem> 
            { 
                new SettingItem { Name = "智能清单",  SettingItemType = SettingItemTypeEnum.IntelligentSetting  } 
            };
            this.SettingItemProfile = new ObservableCollection<SettingItem>
            {
                new SettingItem { Name = "设置邮箱",  SettingItemType = SettingItemTypeEnum.EmailSetting  } ,
                new SettingItem { Name = "修改邮箱",  SettingItemType = SettingItemTypeEnum.ModifiedEmail  } ,
                new SettingItem { Name = "修改密码",  SettingItemType = SettingItemTypeEnum.ModifiedPasword  } 
            };
            this.SettingItemHelps = new ObservableCollection<SettingItem>
            {
                new SettingItem { Name = "使用手册",  SettingItemType = SettingItemTypeEnum.HelpSetting  } ,
                new SettingItem { Name = "帮助中心",  SettingItemType = SettingItemTypeEnum.HelpSetting  } ,
                new SettingItem { Name = "反馈",  SettingItemType = SettingItemTypeEnum.HelpSetting  } 
            };
            this.SettingItemAbout = new ObservableCollection<SettingItem>
            {
                new SettingItem { Name = "滴答清单",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
                new SettingItem { Name = "访问官网",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
                new SettingItem { Name = "使用条款",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
                new SettingItem { Name = "隐私声明",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
                new SettingItem { Name = "开源协议",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
                new SettingItem { Name = "致谢",  SettingItemType = SettingItemTypeEnum.AboutSetting  } ,
            };
        }

        public async Task GetUserProfile()
        {
            UserProfile = await UserProfileBll.GetLastOneUserProfileInfoByUserId(App.SignUserInfo.Sid);
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
