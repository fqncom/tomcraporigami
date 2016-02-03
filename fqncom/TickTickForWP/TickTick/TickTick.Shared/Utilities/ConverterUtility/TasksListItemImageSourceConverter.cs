using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksListItemImageSourceConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tasks = value as Tasks;
            if (tasks == null)
            {
                return string.Empty;
            }
            bool flag = false;
            bool isForVisibility = false;
            bool isForImageSource = false;
            var param = parameter.ToString();
            if (param.Contains("Visibility"))
            {
                isForVisibility = true;
            }
            else
            {
                isForImageSource = true;
            }
            if (param.Contains("Reminder"))//reminder_icon_enable_light.png
            {
                flag = tasks.IsReminder();
            }
            if (param.Contains("Note"))// fqn_diff   :note_small_undone_icon_dark.png 改成了    note_icon_enable_light.png
            {
                flag = !string.IsNullOrEmpty(tasks.Content);
            }
            if (param.Contains("Repeat"))//repeat_icon_enable_light.png
            {
                flag = tasks.IsRepeatTask();
            }

            if (isForImageSource && flag)
            {
                return string.Format("ms-appx:///Assets/Images/Scale-100/{0}_icon_enable_light.png", parameter.ToString().ToLower());
            }
            if (isForVisibility && flag)
            {
                return Windows.UI.Xaml.Visibility.Visible;
            }

            if (param.Contains("Visibility"))
            {
                return Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
