using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksListItemCompleteIconSourceConverter : IValueConverter
    {

        #region IValueConverter 成员

        private static BitmapImage CheckButtonOn;
        private static BitmapImage CheckButtonOff;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var flag = (bool?)value ?? true;

                var basePath = "ms-appx:///Assets/Images/Scale-100/";
                if (flag)
                {
                    return
                        CheckButtonOn =
                            CheckButtonOn ??
                            new BitmapImage(new Uri(string.Format(basePath + "{0}", "btn_check_buttonless_on.png")));
                }
                return
                    CheckButtonOff =
                        CheckButtonOff ??
                        new BitmapImage(
                            new Uri(string.Format(basePath + "{0}", "widget_btn_check_buttonless_off_blue.png")));
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
