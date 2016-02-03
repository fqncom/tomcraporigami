using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using TickTick.Helper;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    /// <summary>
    /// tasksListItem的日期转换辅助类
    /// </summary>
    public class TasksListItemDateConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime? dateTime = value as DateTime?;
            if (dateTime == null)
            {
                //return string.Format("日期未知：{0}", value);
                return null;
            }
            //if (parameter.Equals("Visibility") && dateTime == null)
            //{
            //    return Windows.UI.Xaml.Visibility.Collapsed;
            //}
            if (dateTime.Value.Date.Equals(DateTime.UtcNow.Date))
            {
                //return string.Format("{0}", dateTime.Value.ToLocalTime().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern));
                return string.Format("{0}", dateTime.Value.ToLocalTime().ToString("HH:mm"));

            }
            //if (LoggerHelper.IS_LOG_ENABLED)
            //{
            //    LoggerHelper.LogToAllChannels(null, string.Format("日期格式为：{0}", dateTime.Value.ToString(CultureInfo.CurrentCulture.DateTimeFormat.MonthDayPattern)));
            //}
            //return string.Format("{0}", dateTime.Value.ToLocalTime().ToString(CultureInfo.CurrentCulture.DateTimeFormat.MonthDayPattern));
            return string.Format("{0}", dateTime.Value.ToLocalTime().ToString("dd/MM"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateTime = value as DateTime?;
            return dateTime;
        }

        #endregion
    }
}
