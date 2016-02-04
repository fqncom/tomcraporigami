using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Utilities.ConverterUtility
{
    public class ProjectsListItemConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var projects = value as Projects;

            try
            {
                var basePath = "ms-appx:///Assets/Images/Scale-100/{0}";
                if (projects == null || !projects.IsIntelligentProjects)
                {
                    return string.Format(basePath, "list_edit_index_light.png");
                }
                switch (projects.IntelligentProjectsTypeEnum)
                {
                    case IntelligentProjectsTypeEnum.IsShowTodayList:
                        return string.Format(basePath, "ic_action_today_pressed1.png");
                        break;
                    case IntelligentProjectsTypeEnum.IsShowCompletedList:
                        return string.Format(basePath, "list_edit_index_light1.png");
                        break;
                    case IntelligentProjectsTypeEnum.IsShowScheduledList:
                        return string.Format(basePath, "list_edit_index_light1.png");// TODO 这个没有
                        break;
                    case IntelligentProjectsTypeEnum.IsShow7DaysList:
                        return string.Format(basePath, "list_edit_index_light1.png");
                        break;
                    case IntelligentProjectsTypeEnum.IsShowAllList:
                        return string.Format(basePath, "all_edit_index_light1.png");
                        break;
                    case IntelligentProjectsTypeEnum.IsInboxList:
                        return string.Format(basePath, "inbox_edit_index_light1.png");
                        break;
                }
                return string.Format(basePath + "{0}", "list_edit_index_light1.png");
            }
            catch (Exception e)
            {
                
                throw e;
            }
            //if (!projects.IsIntelligentProjects)
            //{
            //    return string.Format(basePath + "{0}", "list_edit_index_light.png");
            //}
            //if (string.Equals(projectsName, "所有"))
            //{
            //    return string.Format(basePath + "{0}", "all_edit_index_light.png");
            //}
            //if (string.Equals(projectsName, "今天"))
            //{
            //    return string.Format(basePath + "{0}", "inbox_edit_index_light.png");
            //}
            //if (string.Equals(projectsName, "收集箱"))
            //{
            //    return string.Format(basePath + "{0}", "inbox_edit_index_light.png");
            //}
            //if (string.Equals(projectsName, "最近七天"))
            //{
            //    return string.Format(basePath + "{0}", "list_edit_index_light.png");
            //}
            //if (string.Equals(projectsName, "已完成"))
            //{
            //    return string.Format(basePath + "{0}", "list_edit_index_light.png");
            //}
            //return string.Format(basePath + "{0}", "list_edit_index_light.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
