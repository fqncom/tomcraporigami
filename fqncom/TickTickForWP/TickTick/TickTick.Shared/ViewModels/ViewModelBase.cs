using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TickTick.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// 属性变化
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        /// <summary>
        /// 设置属性变化，内部通知修改
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="current">当前属性</param>
        /// <param name="value">新设置的值</param>
        /// <param name="propertyName">属性名称</param>
        public void SetProperty<T>(ref T current, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(current,value))
            {
                return;
            }
            current = value;
            // 通知修改 
            OnPropertyChanged(propertyName);
        }
    }
}
