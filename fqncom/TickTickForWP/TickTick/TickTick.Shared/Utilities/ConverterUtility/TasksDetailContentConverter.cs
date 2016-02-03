using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksDetailContentConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var checkListItemList = value as List<ChecklistItem>;
            if (string.Equals(parameter, "ToContent"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in checkListItemList)
                {
                    sb.AppendFormat("{0}\r\n", item.Title);
                }
                var result = sb.ToString();
                return result = result.Remove(result.Length - 2, 2);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
