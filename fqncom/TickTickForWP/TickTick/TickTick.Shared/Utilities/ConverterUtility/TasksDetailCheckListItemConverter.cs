using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using Windows.UI.Xaml.Data;

namespace TickTick.Utilities.ConverterUtility
{
    public class TasksDetailCheckListItemConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var checkListItem = value as ChecklistItem;

                if (checkListItem == null)
                {
                    return false;
                }

                if (string.Equals(parameter, "IsChecked"))
                {
                    return checkListItem.IsChecked;
                }
                return string.Empty;
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
