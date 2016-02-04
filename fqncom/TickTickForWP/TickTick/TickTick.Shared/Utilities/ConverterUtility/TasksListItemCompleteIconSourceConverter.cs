using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksListItemCompleteIconSourceConverter : IValueConverter
    {

        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var flag = (bool?)value;
            try
            {
                if (flag == null)
                {
                    flag = true;
                }
                var basePath = "ms-appx:///Assets/Images/Scale-100/";
                if (flag.Value)
                {
                    return string.Format(basePath + "{0}", "btn_check_buttonless_on.png");
                }
                else
                {
                    return string.Format(basePath + "{0}", "widget_btn_check_buttonless_off_blue.png");
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
