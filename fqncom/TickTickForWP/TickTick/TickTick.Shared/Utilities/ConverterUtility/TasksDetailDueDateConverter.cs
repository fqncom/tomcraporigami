using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksDetailDueDateConverter : IValueConverter
    {


        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dueDate = value as DateTime?;
            if (dueDate == null)
            {
                dueDate = DateTime.UtcNow;
            }
            dueDate = dueDate.Value.ToLocalTime();
            if (parameter.Equals("DueDate"))
            {
                return DateTime.SpecifyKind(dueDate.Value.Date, DateTimeKind.Utc);
            }
            //else if (parameter.Equals("DueTime"))

            return dueDate.Value.TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
