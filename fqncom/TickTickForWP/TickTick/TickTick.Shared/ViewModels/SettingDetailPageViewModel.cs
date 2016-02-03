using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Models;

namespace TickTick.ViewModels
{
    public class SettingDetailPageViewModel:INotifyPropertyChanged
    {
        private UserProfileBll UserProfileBll = new UserProfileBll();
        public SettingItem SettingItem { set; get; }
        private UserProfile _userProfile;
        public UserProfile UserProfile {
            get { return _userProfile;}
            set {
                if (_userProfile!=value)
                {
                    _userProfile = value;
                    OnPropertyChanged("UserProfile");
                }
            }
        }
        public SettingDetailPageViewModel()
        {
            this.SettingItem = new SettingItem();
            _userProfile = new UserProfile();
        }

        /// <summary>
        /// 根据toggleSwitch中的tag判断要设置的内容，同时根据toggleSwitch是否启用来做调整
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public async Task ChangeTasksListShowContent(string tag, bool isOn)
        {
            if (this.UserProfile==null)
            {
                return;
            }
            switch (tag)
            {
                case"All":
                    this.UserProfile.IsShowAllList = isOn;
                    break;
                case "Today":
                    this.UserProfile.IsShowTodayList = isOn ? ModelStatusEnum.YES : ModelStatusEnum.NO;
                    break;
                case "Last7Days":
                    this.UserProfile.IsShow7DaysList = isOn ? ModelStatusEnum.YES : ModelStatusEnum.NO;
                    break;
                case "Completed":
                    this.UserProfile.IsShowCompletedList = isOn ? ModelStatusEnum.YES : ModelStatusEnum.NO;
                    break;
                default:
                    this.UserProfile.IsShowAllList = isOn;
                    break;
            }
            await SaveProfileChanged();
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

        public async Task SaveProfileChanged()
        {
            await UserProfileBll.SaveUpdateUserProfile(UserProfile);
        }
    }
}
