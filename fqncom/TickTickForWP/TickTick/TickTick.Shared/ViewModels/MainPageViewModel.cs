using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Library;
using TickTick.Models;
using TickTick.Synchronous;
using Windows.ApplicationModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using TickTick.Helper;

namespace TickTick.ViewModels
{
    /// <summary>
    /// 主界面ViewModel类
    /// </summary>
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region 自定义属性
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<Projects> _intelligentProjects;
        /// <summary>
        /// 智能清单
        /// </summary>
        public IList<Projects> IntelligentProjects
        {
            get { return _intelligentProjects; }
            set
            {
                if (_intelligentProjects != value)
                {
                    _intelligentProjects = (List<Projects>)value;
                    OnPropertyChanged("IntelligentProjects");
                }
            }
        }
        private List<Projects> _projects;

        /// <summary>
        /// 用于显示的Projects集合
        /// </summary>
        public IList<Projects> Projects
        {
            get { return _projects; }
            set
            {
                if (_projects != value)
                {
                    _projects = (List<Projects>)value;
                    OnPropertyChanged("Projects");
                }
            }
        }

        private List<Tasks> _tasks;
        /// <summary>
        /// 用于显示的Tasks集合
        /// </summary>
        public IList<Tasks> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != value)
                {
                    _tasks = (List<Tasks>)value;
                    OnPropertyChanged("Tasks");
                }
            }
        }


        private List<Tasks> _tasksFinished;
        /// <summary>
        /// 用于显示的已完成Tasks集合
        /// </summary>
        public IList<Tasks> TasksFinished
        {
            get { return _tasksFinished; }
            set
            {
                if (_tasksFinished != value)
                {
                    _tasksFinished = (List<Tasks>)value;
                    OnPropertyChanged("TasksFinished");
                }
            }
        }


        /// <summary>
        /// 用于存储需要被提示的信息
        /// </summary>
        public IList<Tasks> TasksNeedNotification { get; set; }
        /// <summary>
        /// 当前页面的Projects，默认是“所有”
        /// </summary>
        private Projects _projectsSelected;

        public Projects ProjectsSelected
        {
            get { return _projectsSelected; }
            set
            {
                if (_projectsSelected != value)
                {
                    _projectsSelected = value;
                    OnPropertyChanged("ProjectsSelected");
                }
            }
        }

        //public Projects ProjectsSelected { get; set; }
        /// <summary>
        /// toast传来的数据，之后使用列表list来展示
        /// </summary>
        private Tasks _toastTasks;

        public Tasks ToastTasks
        {
            get { return _toastTasks; }
            set
            {
                _toastTasks = value;
                OnPropertyChanged("ToastTasks");
            }
        }

        /// <summary>
        /// Projects的bll层对象
        /// </summary>
        private ProjectBll ProjectBll = new ProjectBll();
        /// <summary>
        /// Tasks的bll层对象
        /// </summary>
        private TaskBll TaskBll = new TaskBll();

        public ReminderTaskBll ReminderTaskBll = new ReminderTaskBll();
        /// <summary>
        /// 存储用户信息
        /// </summary>
        private User _userInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public User UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;
                OnPropertyChanged("UserInfo");
            }
        }

        public UserProfile UserProfile { get; set; }

        //public User UserInfo { get; set; }
        /// <summary>
        /// 进行同步操作
        /// </summary>
        //public Communicator Communicator { get; set; }

        public SyncBll SyncBll = new SyncBll();
        public SyncStatusBll SyncStatusBll = new SyncStatusBll();
        public UserProfileBll UserProfileBll = new UserProfileBll();
        #endregion

        public MainPageViewModel()
        {
            _projects = new List<Projects>();
            _tasks = new List<Tasks>();
            _tasksFinished = new List<Tasks>();
            TasksNeedNotification = new List<Tasks>();

            _intelligentProjects = new List<Projects>();

            ProjectsSelected = Entity.Projects.GetDefaultProjects();

        }

        public async Task InitialAsync()
        {
            //同步方式默认先自动
            await SyncBll.DoSyncAll(App.SignUserInfo.Sid, Constants.SyncType.SYNC_TYPE_AUTO);
        }
        #region 对projects的业务逻辑操作
        /// <summary>
        /// 删除projects，到垃圾箱去
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        public async Task DeleteProjects(Projects projects)
        {
            await ProjectBll.DeleteForeverWithTasks(projects);
        }

        /// <summary>
        /// 查询所有projects
        /// </summary>
        public async Task LoadProjectsList()
        {
            this.Projects = await ProjectBll.GetAllProjectsWithTasksCount();

            this.IntelligentProjects = await InitialIntelligentProjects();

        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="projects"></param>
        public async Task AddProjects(Projects projects)
        {
            if (await ProjectBll.InsertAsync(projects) > 0)
            {
                Projects.Add(projects);
            }
        }
        public async Task UpdateProjects(Projects projects)
        {
            await ProjectBll.UpdateAsync(projects);
        }

        #endregion


        #region 对tasks的业务逻辑操作
        /// <summary>
        /// 查询所有tasks
        /// </summary>
        public async Task GetAllTasksList()
        {
            await GetTasksByProjectId(string.Empty);
        }

        /// <summary>
        /// 根据projectsId查询tasks
        /// </summary>
        public async Task GetTasksByProjectId(string proId)
        {
            await GetTasksByProjectId(proId, TasksSortEnum.Custom_Sort);
        }
        public async Task GetTasksByProjectId(string proId, TasksSortEnum tasksSortEnum)
        {
            List<Tasks> projectTasksList = new List<Tasks>();

            if (ProjectsSelected.IsIntelligentProjects)
            {
                switch (ProjectsSelected.IntelligentProjectsTypeEnum)
                {
                    case IntelligentProjectsTypeEnum.IsShowTodayList:
                        projectTasksList = await TaskBll.GetTodayTasks();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowCompletedList:
                        projectTasksList = await TaskBll.GetCompletedTasks();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowScheduledList:
                        if (LoggerHelper.IS_LOG_ENABLED)
                        {
                            await LoggerHelper.LogToAllChannels(null, "此处不应该被触发");
                        }
                        projectTasksList = projectTasksList.Where(t => t.DueDate != null).ToList();
                        break;
                    case IntelligentProjectsTypeEnum.IsShow7DaysList:
                        projectTasksList = await TaskBll.GetSevenTasks();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowAllList:
                        projectTasksList = await TaskBll.GetAllTasks();
                        break;
                    case IntelligentProjectsTypeEnum.IsInboxList:
                        projectTasksList = await TaskBll.GetInboxTasks();
                        break;
                }
            }
            else
            {
                projectTasksList = await TaskBll.GetTasksByProjectIdAndSortOrder(proId, tasksSortEnum);
            }

            this.Tasks = projectTasksList.FindAll(t => t.TaskStatus == ModelStatusEnum.NOT_COMPLETED);

            this.TasksFinished = projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.COMPLETED);

            ////1.获取所有的需要进行提醒的任务
            this.TasksNeedNotification = projectTasksList.FindAll((t) => t.IsReminder() && t.TaskStatus == ModelStatusEnum.NOT_COMPLETED);
        }



        /// <summary>
        /// 删除某个tasks
        /// </summary>
        public async Task FinishTask(Tasks task)
        {
            int index = Tasks.IndexOf(task);
            int indexFinished = TasksFinished.IndexOf(task);
            if (index < 0 && indexFinished < 0)
            {
                return;
            }
            if (task.IsCompleted)
            {
                task.TaskStatus = ModelStatusEnum.NOT_COMPLETED;
                TasksFinished.RemoveAt(indexFinished);
                Tasks.Insert(0, task);
            }
            else
            {
                task.TaskStatus = ModelStatusEnum.COMPLETED;
                Tasks.RemoveAt(index);
                TasksFinished.Insert(0, task);
            }

            await ReminderTaskBll.UpdateTaskStatus(task);
        }


        #endregion

        /// <summary>
        /// 推迟通知时间
        /// </summary>
        /// <param name="snoozeBackValue"></param>
        public async Task SnoozeBackReminderTime(int snoozeBackValue)
        {
            ToastTasks.SnoozeRemindTime = DateTime.UtcNow.AddMinutes(snoozeBackValue);// 当前时间加上推迟时间，得到下次提醒时间

            await TaskBll.UpdateTaskContent(ToastTasks);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="tasksSortEnum"></param>
        /// <returns></returns>
        public async Task LoadData(TasksSortEnum tasksSortEnum)
        {
            await Task.WhenAll(LoadProjectsList(), GetTasksByProjectId(ProjectsSelected.Id.ToString()));
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// 获取用户的设置信息
        /// </summary>
        /// <returns></returns>
        public async Task GetUserProfile()
        {
            var userId = App.SignUserInfo.Sid;
            UserProfile = await UserProfileBll.GetLastOneUserProfileInfoByUserId(userId) ??
                          UserProfile.CreateDefaultUserProfile(userId);
        }

        /// <summary>
        /// 初始化智能清单列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Projects>> InitialIntelligentProjects()
        {
            var intelligentProjects = new List<Projects>();
            if (UserProfile == null)
            {
                UserProfile = UserProfile.CreateDefaultUserProfile(App.SignUserInfo.Sid);
            }
            if (UserProfile.IsShowAllList)
            {
                var allTasks = await TaskBll.GetAllTasks();
                intelligentProjects.Add(new Projects
                {
                    Name = "所有",
                    IsIntelligentProjects = true,
                    IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowAllList,
                    TasksCount = allTasks.Count(t => !t.IsCompleted)
                });
            }
            if (UserProfile.IsShowTodayList == ModelStatusEnum.YES)
            {
                var todayTasks = await TaskBll.GetTodayTasks();
                intelligentProjects.Add(new Projects
                {
                    Name = "今天",
                    IsIntelligentProjects = true,
                    IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowTodayList,
                    TasksCount = todayTasks.Count(t => !t.IsCompleted)
                });
            }
            if (UserProfile.IsShow7DaysList == ModelStatusEnum.YES)
            {
                var sevenDaysTasks = await TaskBll.GetSevenTasks();
                intelligentProjects.Add(new Projects
                {
                    Name = "最近7天",
                    IsIntelligentProjects = true,
                    IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShow7DaysList,
                    TasksCount = sevenDaysTasks.Count(t => !t.IsCompleted)
                });
            }
            if (UserProfile.IsShowCompletedList == ModelStatusEnum.YES)
            {
                var completedTasks = await TaskBll.GetCompletedTasks();
                intelligentProjects.Add(new Projects
                {
                    Name = "已完成",
                    IsIntelligentProjects = true,
                    IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowCompletedList,
                    TasksCount = completedTasks.Count(t => !t.IsCompleted)
                });
            }

            //if (UserProfile.IsShowScheduledList)
            //{
            //    IntelligentProjects.Add(new Projects { Name = "所有", IsIntelligentProjects = true });
            //}
            var inboxTasks = await TaskBll.GetInboxTasks();
            intelligentProjects.Add(new Projects
            {
                Name = "收集箱",
                IsIntelligentProjects = true,
                IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsInboxList,
                TasksCount = inboxTasks.Count(t => !t.IsCompleted)
            });
            return intelligentProjects;
        }

    }


}
