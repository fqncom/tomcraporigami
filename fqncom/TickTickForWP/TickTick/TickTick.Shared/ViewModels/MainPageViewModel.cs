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

namespace TickTick.ViewModels
{
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

        private ObservableCollection<Projects> _projects;

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
                    _projects = (ObservableCollection<Projects>)value;
                    OnPropertyChanged("Projects");
                }
            }
        }

        public void BatchAddProjects(IEnumerable<Projects> newProjects)
        {
            // TODO 有问题，需要解决
            _projects.Clear();
            var currentProjects = _projects;
            this.Projects = null;
            foreach (var item in newProjects)
            {
                if (currentProjects.Contains(item))
                {
                    continue;
                }
                currentProjects.Add(item);
            }
            this.Projects = currentProjects;
        }

        private ObservableCollection<Tasks> _tasks;
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
                    _tasks = (ObservableCollection<Tasks>)value;
                    OnPropertyChanged("Tasks");
                }
            }
        }

        public void BatchAddTasks(IEnumerable<Tasks> newTasks)
        {
            // TODO 有问题，需要解决
            _tasks.Clear();
            var currentTasks = _tasks;
            this.Tasks = null;
            foreach (var item in newTasks)
            {
                if (currentTasks.Contains(item))
                {
                    continue;
                }
                currentTasks.Add(item);
            }
            this.Tasks = currentTasks;
        }

        private ObservableCollection<Tasks> _tasksFinished;
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
                    _tasksFinished = (ObservableCollection<Tasks>)value;
                    OnPropertyChanged("TasksFinished");
                }
            }
        }

        public void BatchAddTasksFinished(IEnumerable<Tasks> newTasksFinished)
        {
            // TODO 有问题，需要解决
            _tasksFinished.Clear();
            var currentTasksFinished = _tasksFinished;
            this.TasksFinished = null;
            foreach (var item in newTasksFinished)
            {
                if (currentTasksFinished.Contains(item))
                {
                    continue;
                }
                currentTasksFinished.Add(item);
            }
            this.TasksFinished = currentTasksFinished;
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

        private ObservableCollection<Projects> _intelligentcoProjects;
        /// <summary>
        /// 智能清单
        /// </summary>
        public IList<Projects> IntelligentProjects
        {
            get { return _intelligentcoProjects; }
            set
            {
                if (_intelligentcoProjects != value)
                {
                    _intelligentcoProjects = (ObservableCollection<Projects>)value;
                    OnPropertyChanged("IntelligentcoProjects");
                }
            }
        }

        //public void UpdateIntelligentProjects(IEnumerable<Projects> newIntelligentProjects)
        //{
        //    _intelligentcoProjects = null;
        //    var current = _intelligentcoProjects;
        //    IntelligentProjects = null;
        //    foreach (var item in newIntelligentProjects)
        //    {
        //        current.Add(item);
        //    }
        //    IntelligentProjects = current;
        //}

        //public Tasks ToastTasks { get; set; }


        #region 弃用
        ///// <summary>
        ///// 用户判断projects列表的显示与隐藏状态
        ///// </summary>
        //public ProjectsListStatus ProjectsListStatus { get; set; } 
        #endregion

        /// <summary>
        /// 用户dal层对象
        /// </summary>
        //private IBaseBll<User> UserBll { get; set; }
        /// <summary>
        /// Projects的dal层对象
        /// </summary>
        private ProjectBll ProjectBll = new ProjectBll();
        /// <summary>
        /// Tasks的dal层对象
        /// </summary>
        private TaskBll TaskBll = new TaskBll();
        //private IBaseDal<Tasks> TaskDal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ReminderTaskBll ReminderTaskBll = new ReminderTaskBll();
        /// <summary>
        /// 存储用户信息
        /// </summary>
        private User _userInfo;

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
            _projects = new ObservableCollection<Projects>();
            _tasks = new ObservableCollection<Tasks>();
            _tasksFinished = new ObservableCollection<Tasks>();
            TasksNeedNotification = new List<Tasks>();

            _intelligentcoProjects = new ObservableCollection<Projects>();
            //Projects = new TrulyObservableCollection<Projects>();
            //Tasks = new TrulyObservableCollection<Entity.Tasks>();
            //TasksFinished = new TrulyObservableCollection<Entity.Tasks>();
            //TasksNeedNotification = new ObservableCollection<Entity.Tasks>();
            ProjectsSelected = Entity.Projects.GetDefaultProjects();

            if (DesignMode.DesignModeEnabled)
            {

            }

            //ProjectBll = new ProjectBll();
            //TaskBll = new TaskBll();

            //初始化同步数据
            //InitialAsync();

            #region 测试用数据 弃用

#if DEBUG

            //Communicator = new Communicator();

            //直接删除表数据，测试使用！！！=============
            //ProjectDal.DropTable();
            //TaskDal.DropTable();


            //插入测试数据
            //ProjectDal.InsertAllAsync(new List<Projects> { 
            //    new Projects{ ProjectName="test1"},
            //    new Projects{ ProjectName="test2"},
            //    new Projects{ ProjectName="test3"},
            //});

            //TaskDal.InsertAllAsync(new List<Tasks> {
            //    new Tasks{ TaskName = "task3", ProjectId = 3},
            //    new Tasks{ TaskName = "task3", ProjectId = 4},
            //    new Tasks{ TaskName = "task3", ProjectId = 5},
            //    new Tasks{ TaskName = "task3", ProjectId = 3},
            //    new Tasks{ TaskName = "task3", ProjectId = 4},
            //    new Tasks{ TaskName = "task3", ProjectId = 5},
            //});

            ////从数据库中查询出所有的projects
            //GetAllProjects();

            ////从数据库中查询出所有的tasks
            //GetAllTasks();
#endif

            #endregion
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
        public async Task GetAllProjects()
        {
            var projectsTemp = await ProjectBll.GetAllProjectsWithTasksCount();
            //var projectsTemp = await ProjectBll.GetAllProjectsDeletedNo();

            BatchAddProjects(projectsTemp);

            //Projects.Clear();
            //foreach (var item in projectsTemp)
            //{
            //    Projects.Add(item);
            //}
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

        #region 弃用
        /// <summary>
        /// 删除某个projects
        /// </summary>
        //public async Task DeleteProject(Projects projects)
        //{
        //    if (await ProjectBll.DeleteForever(projects) > 0)
        //    {
        //        //System.Diagnostics.Debug.WriteLine("删除一条projects成功");
        //    }
        //} 
        #endregion

        #endregion


        #region 对tasks的业务逻辑操作
        /// <summary>
        /// 查询所有tasks
        /// </summary>
        public async Task GetAllTasks()
        {
            //var allTasksList = await TaskBll.GetAllTasks();
            //Tasks = new ObservableCollection<Tasks>(allTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.Task_Not_Finished));
            ////TasksFinished = new ObservableCollection<Tasks>(await TaskBll.GetAllTasksByTaskStatus(ModelStatusEnum.Task_Finished));
            //TasksFinished = new ObservableCollection<Tasks>(allTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.Task_Finished));

            ////1.获取所有的需要进行提醒的任务
            //TasksNeedNotification = new ObservableCollection<Tasks>(allTasksList.FindAll((t) => t.IsReminder() && t.TaskStatus == ModelStatusEnum.Task_Not_Finished));
            await GetTasksByProjectId(string.Empty);
        }

        /// <summary>
        /// 根据projectsId查询tasks
        /// </summary>
        public async Task GetTasksByProjectId(string proId)
        {
            //var projectTasksList = await TaskBll.GetAllTasksByProjectId(proId);
            //Tasks = new ObservableCollection<Tasks>(projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.Task_Not_Finished));
            //TasksFinished = new ObservableCollection<Tasks>(projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.Task_Finished));

            ////1.获取所有的需要进行提醒的任务
            //TasksNeedNotification = new ObservableCollection<Tasks>(projectTasksList.FindAll((t) => t.IsReminder() && t.TaskStatus == ModelStatusEnum.Task_Not_Finished));
            await GetTasksByProjectId(proId, TasksSortEnum.Custom_Sort);
        }
        public async Task GetTasksByProjectId(string proId, TasksSortEnum tasksSortEnum)
        {
            //if (string.IsNullOrEmpty(proId))
            //{
            //    proId = ProjectsSelected.Id.ToString();
            //}
            var projectTasksList = await TaskBll.GetAllTasksByProjectIdDeletedNo(proId, tasksSortEnum);
            //Tasks = new List<Entity.Tasks>(projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.NOT_COMPLETED));
            //TasksFinished = new List<Entity.Tasks>(projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.COMPLETED));

            if (ProjectsSelected.IntelligentProjectsTypeEnum == IntelligentProjectsTypeEnum.IsInboxList)
            {
                foreach (var item in IntelligentProjects)
                {
                    switch (item.IntelligentProjectsTypeEnum)
                    {
                        case IntelligentProjectsTypeEnum.IsShowTodayList:
                            item.TasksCount = projectTasksList.Count(t => t.CreatedTime.Date == DateTime.UtcNow);
                            break;
                        case IntelligentProjectsTypeEnum.IsShowCompletedList:
                            item.TasksCount = projectTasksList.Count(t => t.IsCompleted);
                            break;
                        case IntelligentProjectsTypeEnum.IsShowScheduledList:
                            item.TasksCount = projectTasksList.Count(t => t.DueDate != null);
                            break;
                        case IntelligentProjectsTypeEnum.IsShow7DaysList:
                            item.TasksCount = projectTasksList.Count(t => DateTime.UtcNow.Date - t.CreatedTime.Date <= TimeSpan.FromDays(7));
                            break;
                        case IntelligentProjectsTypeEnum.IsShowAllList:
                            item.TasksCount = projectTasksList.Count;
                            break;
                        case IntelligentProjectsTypeEnum.IsInboxList:
                            var inboxId = App.SignUserInfo.InboxId;
                            item.TasksCount = projectTasksList.Count(t => t.ProjectSid == inboxId);
                            break;
                        default:
                            item.TasksCount = projectTasksList.Count;
                            break;
                    }
                }
            }
            //var tasksTemp = projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.NOT_COMPLETED);


            if (ProjectsSelected.IsIntelligentProjects)
            {
                switch (ProjectsSelected.IntelligentProjectsTypeEnum)
                {
                    case IntelligentProjectsTypeEnum.IsShowTodayList:
                        projectTasksList = projectTasksList.Where(t => t.CreatedTime.Date == DateTime.UtcNow).ToList();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowCompletedList:
                        projectTasksList = projectTasksList.Where(t => t.IsCompleted).ToList();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowScheduledList:
                        projectTasksList = projectTasksList.Where(t => t.DueDate != null).ToList();
                        break;
                    case IntelligentProjectsTypeEnum.IsShow7DaysList:
                        projectTasksList = projectTasksList.Where(t => DateTime.UtcNow.Date - t.CreatedTime.Date <= TimeSpan.FromDays(7)).ToList();
                        break;
                    case IntelligentProjectsTypeEnum.IsShowAllList:
                        break;
                    case IntelligentProjectsTypeEnum.IsInboxList:
                        var inboxId = App.SignUserInfo.InboxId;
                        projectTasksList = projectTasksList.Where(t => t.ProjectSid == inboxId).ToList();
                        break;
                }
            }
            else
            {
                projectTasksList = projectTasksList.FindAll(t => t.ProjectId == ProjectsSelected.Id.ToString());

            }


            BatchAddTasks(projectTasksList.FindAll(t => t.TaskStatus == ModelStatusEnum.NOT_COMPLETED));
            //UpdateIntelligentProjects();
            //Tasks.Clear();
            //foreach (var item in tasksTemp)
            //{
            //    Tasks.Add(item);
            //}

            var tasksFinishedTemp = projectTasksList.FindAll((t) => t.TaskStatus == ModelStatusEnum.COMPLETED);

            BatchAddTasksFinished(tasksFinishedTemp);
            //TasksFinished.Clear();
            //foreach (var item in tasksFinishedTemp)
            //{
            //    TasksFinished.Add(item);
            //}

            ////1.获取所有的需要进行提醒的任务
            TasksNeedNotification = projectTasksList.FindAll((t) => t.IsReminder() && t.TaskStatus == ModelStatusEnum.NOT_COMPLETED);
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


        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="tasks"></param>
        public async Task AddTasks(Tasks tasks)
        {
            await Task.WhenAll(TaskBll.InsertAsync(tasks), SyncStatusBll.InsertAsync(new SyncStatus { EntityId = tasks.SId, Type = ModelStatusEnum.SYNC_TYPE_TASK_CREATE, UserId = tasks.UserId }));
            //await TaskBll.InsertAsync(tasks);
            //await SyncStatusBll.InsertAsync(new SyncStatus { EntityId = tasks.SId, Type = ModelStatusEnum.SYNC_TYPE_TASK_CREATE, UserId = tasks.UserId });
            //await SyncBll.DoAsync();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="tasks"></param>
        //public async Task UpdateTasks(Tasks tasks)
        //{
        //    await TaskBll.UpdateAsync(tasks);
        //    await SyncStatusBll.InsertAsync(new SyncStatus { EntityId = tasks.SId, Type = ModelStatusEnum.SYNC_TYPE_TASK_CONTENT, UserId = tasks.UserId });
        //    //await SyncBll.DoAsync();
        //}

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

        public async Task GetAllProjectsAndTasks(TasksSortEnum tasksSortEnum)
        {
            //var project = ProjectsSelected;

            await GetAllProjects();
            InitialIntelligentProjects();
            await GetAllTasks();
            
            // await GetAllProjects()
            //if (project.SId.Equals(App.SignUserInfo.InboxId))// TODO 临时用户可能会有问题
            //{
            //    await GetAllTasks();
            //}
            //else
            //{
            //    await GetTasksByProjectId(project.Id.ToString(), tasksSortEnum);
            //}
            //foreach (var item in Projects)
            //{
            //    item.TasksCount = Tasks.Count(t => t.ProjectId == item.Id.ToString());
            //}
            // 初始化智能列表


            //if (Tasks != null)
            //{
            //    if (!project.IsIntelligentProjects)
            //    {
            //        Tasks = Tasks.Where(t => t.ProjectId == project.Id.ToString()).ToList();
            //    }
            //    else
            //    {
            //        switch (project.IntelligentProjectsTypeEnum)
            //        {
            //            case IntelligentProjectsTypeEnum.IsShowTodayList:
            //                Tasks = Tasks.Where(t => t.CreatedTime.Date == DateTime.UtcNow).ToList();
            //                break;
            //            case IntelligentProjectsTypeEnum.IsShowCompletedList:
            //                Tasks = Tasks.Where(t => t.IsCompleted).ToList();
            //                break;
            //            case IntelligentProjectsTypeEnum.IsShowScheduledList:
            //                Tasks = Tasks.Where(t => t.DueDate != null).ToList();
            //                break;
            //            case IntelligentProjectsTypeEnum.IsShow7DaysList:
            //                Tasks = Tasks.Where(t => DateTime.UtcNow.Date - t.CreatedTime.Date <= TimeSpan.FromDays(7)).ToList();
            //                break;
            //            case IntelligentProjectsTypeEnum.IsShowAllList:
            //                Tasks = Tasks;
            //                break;
            //            default:
            //                Tasks = Tasks;
            //                break;
            //        }
            //    }
            //}
            //// 初始化智能列表
            //InitialIntelligentProjects();
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public async Task GetUserProfile()
        {
            var userId = App.SignUserInfo.Sid;
            UserProfile = await UserProfileBll.GetLastOneUserProfileInfoByUserId(userId);
            if (UserProfile == null)
            {
                UserProfile = UserProfile.CreateDefaultUserProfile(userId);
            }
        }

        public void InitialIntelligentProjects()
        {
            IntelligentProjects.Clear();
            if (UserProfile == null)
            {
                UserProfile = UserProfile.CreateDefaultUserProfile(App.SignUserInfo.Sid);
            }
            if (UserProfile.IsShowAllList)
            {
                IntelligentProjects.Add(new Projects { Name = "所有", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowAllList });
            }
            if (UserProfile.IsShowTodayList == ModelStatusEnum.YES)
            {
                IntelligentProjects.Add(new Projects { Name = "今天", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowTodayList });
            }
            if (UserProfile.IsShow7DaysList == ModelStatusEnum.YES)
            {
                IntelligentProjects.Add(new Projects { Name = "最近7天", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShow7DaysList });
            }
            if (UserProfile.IsShowCompletedList == ModelStatusEnum.YES)
            {
                IntelligentProjects.Add(new Projects { Name = "已完成", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsShowCompletedList });
            }

            //if (UserProfile.IsShowScheduledList)
            //{
            //    IntelligentProjects.Add(new Projects { Name = "所有", IsIntelligentProjects = true });
            //}

            IntelligentProjects.Add(new Projects { Name = "收集箱", IsIntelligentProjects = true, IntelligentProjectsTypeEnum = IntelligentProjectsTypeEnum.IsInboxList });
        }

    }


}
