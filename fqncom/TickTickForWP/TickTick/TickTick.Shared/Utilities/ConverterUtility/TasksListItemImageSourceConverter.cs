using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksListItemImageSourceConverter : IValueConverter
    {
        #region IValueConverter 成员

        public static BitmapImage RepeatImage;
        public static BitmapImage RemindImage;
        public static BitmapImage NoteImage;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var tasks = value as Tasks;
                if (tasks == null)
                {
                    return string.Empty;
                }
                
                string basePath = "ms-appx:///Assets/Images/Scale-100/{0}_icon_enable_light.png";
                var param = parameter.ToString();
                
                if (param.Contains("Reminder"))//reminder_icon_enable_light.png
                {
                    if (tasks.IsReminder())
                    {
                        return
                            RemindImage =
                                RemindImage ??
                                new BitmapImage(new Uri(string.Format(basePath, parameter.ToString().ToLower())));
                    }
                }
                if (param.Contains("Note"))// fqn_diff   :note_small_undone_icon_dark.png 改成了    note_icon_enable_light.png
                {
                    if (!string.IsNullOrEmpty(tasks.Content))
                    {
                        return
                         NoteImage =
                             NoteImage ??
                             new BitmapImage(new Uri(string.Format(basePath, parameter.ToString().ToLower())));
                    }
                }
                if (param.Contains("Repeat"))//repeat_icon_enable_light.png
                {
                    if (tasks.IsRepeatTask())
                    {
                        return RepeatImage = RepeatImage ?? new BitmapImage(new Uri(string.Format(basePath, parameter.ToString().ToLower())));
                    }
                }
                return string.Empty;
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
