using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using TickTick.Helper;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

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

            try
            {
                if (dateTime == null)
                {
                    //return string.Format("日期未知：{0}", value);
                    if (parameter.Equals("Foreground"))
                    {
                        return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    }
                    return string.Empty;
                }
                if (parameter != null && parameter.Equals("Foreground"))
                {
                    if (dateTime.Value > DateTime.UtcNow)
                    {
                        return new SolidColorBrush(Color.FromArgb(255, 153, 153, 153));
                    }
                    return new SolidColorBrush(Color.FromArgb(255, 245, 93, 93));
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
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateTime = value as DateTime?;
            return dateTime;
        }

        #endregion
    }
}
