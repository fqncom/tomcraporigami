using DDay.iCal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using TickTick.Enum;
using TickTick.Enums;
using TickTick.Library;
using TickTick.Utilities;
using TickTick.Views;
using TickTick.Views.ViewService;

namespace TickTick.ViewModels
{
    public class TasksDetailPageViewModel : INotifyPropertyChanged
    {
        #region 自定义属性
        public ProjectBll ProjectBll = new ProjectBll();
        public CheckListViewService CheckListViewService = new CheckListViewService();
        /// <summary>
        /// 经过修改之后的tasks
        /// </summary>
        private Tasks _tasks;
        public Tasks Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }
        /// <summary>
        /// 未修改的tasks
        /// </summary>
        public Tasks OriginalTasks { get; set; }
        /// <summary>
        /// projects对象
        /// </summary>
        public Projects Projects { get; set; }
        public TaskBll TaskBll = new TaskBll();

        public List<SnoozeTimeSelection> RemindTimeSelectionList { get; set; }
        //public List<> RemindTimeSelectionList { get; set; }
        public List<PrioritySelection> PrioritiesEnumList { get; set; }
        public List<RepeatTimeSelection> RepeatTimeSelectionList { get; set; }

        private ObservableCollection<ChecklistItem> _trulyChecklistItems;
        public IList<ChecklistItem> TrulyCheckListItems
        {
            get
            {
                return _trulyChecklistItems;
            }
            set
            {
                if (_trulyChecklistItems != value)
                {
                    _trulyChecklistItems = (ObservableCollection<ChecklistItem>)value;
                    OnPropertyChanged("TrulyCheckListItems");
                }
            }
        }

        public void BatchAddCheckListItems(IEnumerable<ChecklistItem> newCheckListitem)
        {
            var currentCheckListItem = _trulyChecklistItems;
            this.TrulyCheckListItems = null;
            foreach (var item in newCheckListitem)
            {
                currentCheckListItem.Add(item);
            }
            this.TrulyCheckListItems = currentCheckListItem;
        }

        #endregion

        public TasksDetailPageViewModel()
        {
            _tasks = new Tasks();
            
            this.Projects = new Projects();

            _trulyChecklistItems = new ObservableCollection<ChecklistItem>();
            //初始化界面combox选项
            InitializationComboxList();
        }

        private void InitializationComboxList()
        {
            this.PrioritiesEnumList = SelectionListEnum.PrioritySelectionList;
            this.RemindTimeSelectionList = SelectionListEnum.SnoozeTimeSelectionList;
            this.RepeatTimeSelectionList = SelectionListEnum.RepeatTimeSelection;
        }
        public async Task AddTasks()
        {
            Tasks.ChecklistItems = new List<ChecklistItem>(TrulyCheckListItems);
            await TaskBll.SaveTask(this.OriginalTasks, this.Tasks);
        }
        public async Task UpdateTasks()
        {
            Tasks.ChecklistItems = new List<ChecklistItem>(TrulyCheckListItems);
            await TaskBll.SaveTask(this.OriginalTasks, this.Tasks);
        }
        /// <summary>
        /// 彻底删除任务
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTasks()
        {
            await TaskBll.DeleteForever(this.Tasks);
        }

        public async Task<List<Projects>> GetAllProjects()
        {
            return await ProjectBll.GetAllProjectsDeletedNo();
        }


        public void ChangePriority(PrioritySelection prioritiesItem)
        {
            //PrioritySelection prioritiesValue;

            #region 这里先这样写，之后由于listItem必然会改，所以留着switch
            //switch (prioritiesItem)
            //{
            //    case PrioritiesEnum.HighPriorities:
            //        prioritiesValue = PrioritiesEnum.HighPriorities;
            //        break;
            //    case PrioritiesEnum.MiddlePriorities:
            //        prioritiesValue = PrioritiesEnum.MiddlePriorities;
            //        break;
            //    case PrioritiesEnum.LowPriorities:
            //        prioritiesValue = PrioritiesEnum.LowPriorities;
            //        break;
            //    case PrioritiesEnum.NonePriorities:
            //        prioritiesValue = PrioritiesEnum.NonePriorities;
            //        break;
            //    default:
            //        prioritiesValue = PrioritiesEnum.MiddlePriorities;
            //        break;
            //}
            #endregion


            this.Tasks.Priority = prioritiesItem.PrioritiesEnum;
        }

        public void ChangeRemindTime(SnoozeTimeSelection selectedItem)
        {
            this.Tasks.Reminder = selectedItem.SnoozeValue;
        }

        public void ChangeDueDate(DateTime? newDate)
        {
            if (this.Tasks.DueDate != null)
            {
                UpdateDueDate(newDate, this.Tasks.DueDate.Value.TimeOfDay);
            }
            else
            {
                UpdateDueDate(newDate, DateTime.Now.TimeOfDay);// TODO Utc时间
            }
        }

        public void ChangeDueTime(TimeSpan newTime)
        {
            if (this.Tasks.DueDate != null)
            {
                UpdateDueDate(this.Tasks.DueDate.Value.Date, newTime);
            }
            else
            {
                UpdateDueDate(DateTime.UtcNow.Date, newTime);
            }
        }

        private void UpdateDueDate(DateTime? newDate, TimeSpan? newTime)
        {
            var dueDate = newDate ?? DateTime.Now.Date;
            var dueDateTime = newTime ?? DateTime.Now.TimeOfDay;

            //2015-03-29T16:00:00.000+0000
            //strDate = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"); // 2007-07-21T15:11:19.1250000+05:30   
            var localTime = new DateTime(newDate.Value.Year, newDate.Value.Month, newDate.Value.Day, newTime.Value.Hours, newTime.Value.Minutes, newTime.Value.Seconds, newTime.Value.Milliseconds, DateTimeKind.Local);
            this.Tasks.DueDate = localTime.ToUniversalTime();
            //this.Tasks.DueDate = string.Format("{0}T{1}.000+0000", dueDate.ToStringDateyMd(), (dueDateTime).ToStringTimeSpanHms())).ToUniversalTime();
        }
        public bool Composing;
        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="content"></param>
        public void SaveToTask(string content)
        {
            if (Tasks.IsChecklistMode())
            {
                // task title has been updated
                Tasks.Content = CheckListViewService.GetCompositeContent(Tasks);
            }
            else
            {
                if (Composing)
                {
                    String[] tstr = StringUtils.ParseCompositeNotes(content);
                    Tasks.Title = tstr[0];
                    Tasks.Content = tstr[1];
                }
            }
        }
        /// <summary>
        /// 暂时弃用
        /// </summary>
        /// <param name="kind"></param>
        public void SwitchTaskMode()
        {
            //SetSoftInputAdjust(kind == TickTick.Enums.Constants.Kind.CheckList);
            switch (Tasks.Kind)
            {
                case Constants.Kind.CHECKLIST:
                    //HideSoftImmediately();
                    Tasks.Kind = Constants.Kind.TEXT;
                    Tasks.ChecklistItems = new List<ChecklistItem>(TrulyCheckListItems);
                    Tasks.Content = CheckListViewService.GetCompositeContent(Tasks);
                    //ListViewMode();
                    //adapter.notifyLinkifyChanged();
                    break;
                case Constants.Kind.TEXT:

                    Tasks.Kind = Constants.Kind.CHECKLIST;

                    CheckListViewService.SwitchToChecklist(Tasks);

                    //TrulyCheckListItems = new TrulyObservableCollection<ChecklistItem>(Tasks.ChecklistItems);
                    TrulyCheckListItems.Clear();
                    foreach (var item in Tasks.ChecklistItems)
                    {
                        TrulyCheckListItems.Add(item);
                    }
                    //HideSoftImmediately();
                    //TickTickApplicationBase.getInstance().getChecklistItemService()
                    //        .deleteAllChecklistByTaskId(task.getId());
                    //composeView.setText(Tasks.ToCompositeNote(Tasks.Title, Tasks.Content));
                    //ListViewMode();
                    break;
                default:
                    break;
            }
        }

        public async Task GetProjectsByProjectId()
        {
            this.Projects = await ProjectBll.GetProjectByProjectId(Convert.ToInt32(this.Tasks.ProjectId));
        }

        public async Task GetTasksByTasksId(int tasksId)
        {
            this.Tasks = await TaskBll.GetFullTaskById(tasksId);
            //this.Tasks = await TaskBll.GetTasksWithCheckListByTasksId(tasksId);
        }


        public void SwitchCheckListMode()
        {
            CheckListViewService.SwitchToChecklist(Tasks);
        }

        public void ChangeBelongProjectId(string projectId)
        {
            this.Tasks.ProjectId = projectId;
        }

        public void ChangeTitle(string title)
        {
            this.Tasks.Title = title;
        }

        public void ChangeRepeatTime(int repeatTimeEnum)
        {
            if (repeatTimeEnum.Equals(TaskRepeatItemEnum.DOES_NOT_REPEAT))
            {
                return;
            }
            TickRRule rule = new TickRRule();
            //if (rule == null)
            //{
            //    rule = new TickRRule();
            //    rule.SetInterval(1);
            //}
            var taskDate = DateTime.UtcNow;
            List<IWeekDay> weekdayNums = new List<IWeekDay>();

            switch (repeatTimeEnum)
            {
                case TaskRepeatItemEnum.DOES_NOT_REPEAT:
                    rule = null;
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_NO);
                    //if (!repeatArray.isEmpty())
                    //{
                    //    if (repeatArray.get(repeatArray.size() - 1).type
                    //            == RepeatSettingsItem.ITEM_TYPE_END_REPEAT)
                    //    {
                    //        repeatArray.get(repeatArray.size() - 1).summary = "";
                    //    }
                    //    if (repeatArray.get(repeatArray.size() - 2).type
                    //            == RepeatSettingsItem.ITEM_TYPE_CUSTOM)
                    //    {
                    //        repeatArray.get(repeatArray.size() - 2).summary = "";
                    //    }
                    //}
                    //mSettingsAdapter.notifyDataSetChanged();
                    break;
                case TaskRepeatItemEnum.REPEATS_DAILY:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_DAILY);
                    rule = new TickRRule("RRULE:FREQ=DAILY;INTERVAL=1");
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Daily);
                    break;
                case TaskRepeatItemEnum.REPEATS_EVERY_WEEKDAY:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_WEEKDAY);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Weekly);
                    weekdayNums.Clear();
                    weekdayNums.Add(new WeekDay(DayOfWeek.Monday, 0));
                    weekdayNums.Add(new WeekDay(DayOfWeek.Tuesday, 0));
                    weekdayNums.Add(new WeekDay(DayOfWeek.Wednesday, 0));
                    weekdayNums.Add(new WeekDay(DayOfWeek.Thursday, 0));
                    weekdayNums.Add(new WeekDay(DayOfWeek.Friday, 0));
                    rule.SetByDay(weekdayNums);
                    break;
                case TaskRepeatItemEnum.REPEATS_WEEKLY_ON_DAY:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_WEEKLY);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Weekly);
                    weekdayNums.Clear();
                    weekdayNums.Add(RepeatUtils.CreateWeekdayNum(taskDate));
                    rule.SetByDay(weekdayNums);
                    break;
                case TaskRepeatItemEnum.REPEATS_MONTHLY_ON_DAY_COUNT:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_MONTHLY_WEEK);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Monthly);
                    weekdayNums.Clear();
                    weekdayNums.Add(RepeatUtils.CreateMonthWeekdayNum(taskDate));
                    rule.SetByDay(weekdayNums);
                    break;
                case TaskRepeatItemEnum.REPEATS_MONTHLY_ON_DAY:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_MONTHLY_DAY);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Monthly);
                    if (DateTimeUtils.IsLastDayOfMonth(taskDate))
                    {
                        rule.SetByMonthDay(new int[] { -1 });
                    }
                    break;
                case TaskRepeatItemEnum.REPEATS_YEARLY:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_YEARLY);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetFreq(FrequencyType.Yearly);
                    rule.SetByMonth(new int[] { taskDate.Month + 1 });
                    rule.SetByMonthDay(new int[] { taskDate.Day });
                    break;

                case TaskRepeatItemEnum.REPEATS_YEARLY_LUNAR:
                    //mApplication.getAnalyticsInstance().sendRepeatSetEvent(
                    //        AnalyticsConstants.DateSetupParams.LABEL_REPEAT_YEARLY_LUNAR);
                    rule = new TickRRule();
                    rule.SetInterval(1);
                    rule.SetLunarFrequency(FrequencyType.Yearly);
                    DateTime selected = taskDate;
                    // TODO 农历暂时不动。。。

                    //String time = LunarUtil.SIMPLE_DATE_FORMAT.format(selected);
                    //LunarEntity lunar = null;
                    //try
                    //{
                    //    lunar = LunarUtil.solar2Lunar(time);
                    //}
                    //catch (ParseException e)
                    //{
                    //    e.printStackTrace();
                    //}
                    //if (lunar != null)
                    //{
                    //    rule.setByMonth(new int[] {
                    //        lunar.getMonth()
                    //});

                    //    if (lunar.getDay() == 30)
                    //    {
                    //        rule.setByMonthDay(new int[] {
                    //            -1
                    //    });
                    //    }
                    //    else
                    //    {
                    //        rule.setByMonthDay(new int[] {
                    //            lunar.getDay()
                    //    });
                    //    }
                    //}
                    break;
                case TaskRepeatItemEnum.REPEATS_CUSTOM:
                    // TODO 用户自定义先不做
                    //new CustomRepeatSetDialog(mActivity, dialogSetRRuleCallback)
                    //        .showCustomRepeatDialog();
                    break;
                case TaskRepeatItemEnum.REPEATS_END:
                    // TODO 禁用当前的窗体
                    //if (view.isEnabled())
                    //{
                    //    new RepeatEndViewController(mActivity, repeatEndCallback).showRepeatEndDialog();
                    //}
                    break;
                default:
                    break;
            }
            // 直接设置的repeat选项中，repeatFrom始终是DEFAULT
            if (repeatTimeEnum != TaskRepeatItemEnum.REPEATS_CUSTOM && repeatTimeEnum != TaskRepeatItemEnum.REPEATS_END)
            {
                // TODO 未实现
                //if (!repeatArray.isEmpty())
                //{
                //    if (repeatArray.get(repeatArray.size() - 2).type
                //            == RepeatSettingsItem.ITEM_TYPE_CUSTOM)
                //    {
                //        repeatArray.get(repeatArray.size() - 2).summary = "";
                //    }
                //    if (repeatArray.get(repeatArray.size() - 1).type
                //            == RepeatSettingsItem.ITEM_TYPE_END_REPEAT)
                //    {
                //        repeatArray.get(repeatArray.size() - 1).summary = mActivity.getResources()
                //                .getStringArray(R.array.remind_before_name)[0];
                //    }
                //}
                //mRepeatSetHandler.onRepeatSet(rule, Constants.RepeatFromStatus.DEFAULT);
            }

            //更改选中状态
            //if (item.type != RepeatSettingsItem.ITEM_TYPE_END_REPEAT
            //        && item.type != RepeatSettingsItem.ITEM_TYPE_CUSTOM)
            //{
            //    for (int i = 0; i < repeatArray.size(); i++)
            //    {
            //        if (repeatArray.get(i) != null)
            //        {
            //            repeatArray.get(i).selected = false;
            //        }
            //    }
            //    item.selected = true;
            //}
            //mSettingsAdapter.notifyDataSetChanged();


            // fqn 修改repeat状态
            if (rule == null)
            {
                this.Tasks.RepeatFlag = string.Empty;
                return;
            }
            this.Tasks.RepeatFlag = rule.ToTickTickIcal();

        }

        public int GetRepeatSelectedValue()
        {
            int checkPosition = 0;
            var repeatFlag = Tasks.RepeatFlag;
            if (string.IsNullOrEmpty(repeatFlag))
            {
                return checkPosition;
            }
            TickRRule rRule = new TickRRule(repeatFlag);
            var freq = rRule.GetFreq();
            switch (freq)
            {
                case FrequencyType.Daily:
                    checkPosition = Constants.Repeats.REPEATS_DAILY;
                    break;
                //case FrequencyType.Hourly:
                //    checkPosition = Constants.Repeats.REPEATS_DAILY;
                //    break;
                //case FrequencyType.Minutely:
                //    checkPosition = Constants.Repeats.REPEATS_DAILY;
                //    break;
                case FrequencyType.Monthly:
                    bool onByWeekDay = DateTimeUtils.IsRRuleWeekOnDay(rRule);
                    if (rRule.GetByMonthDay().Count > 0)
                    {
                        return checkPosition;
                    }
                    checkPosition = onByWeekDay ? Constants.Repeats.REPEATS_MONTHLY_ON_DAY_COUNT :
                            Constants.Repeats.REPEATS_MONTHLY_ON_DAY;
                    break;
                //case FrequencyType.None:
                //    checkPosition = Constants.Repeats.REPEATS_DAILY;
                //    break;
                //case FrequencyType.Secondly:
                //    checkPosition = Constants.Repeats.REPEATS_DAILY;
                //    break;
                case FrequencyType.Weekly:
                    IList<IWeekDay> weekdayNums = rRule.GetByDay();
                    bool isWeekDays = DateTimeUtils.IsRRuleWeekdays(weekdayNums);
                    if (isWeekDays)
                    {
                        checkPosition = Constants.Repeats.REPEATS_EVERY_WEEKDAY;
                    }
                    else if (weekdayNums.Count <= 1)
                    {
                        checkPosition = Constants.Repeats.REPEATS_WEEKLY_ON_DAY;
                    }
                    break;
                case FrequencyType.Yearly:
                    if (rRule.IsLunarFrequency())
                    {
                        // TODO 暂时不实现
                        //if (isChinese)
                        //{
                        //    checkPosition = 7;
                        //}
                    }
                    else
                    {
                        checkPosition = 6;
                    }
                    break;
                default:
                    break;
            }
            return checkPosition;
        }

        public void AddNewCheckListItemByEnterKeyDown()
        {
            var checkListItem = new ChecklistItem
            {
                Checked = ModelStatusEnum.NOT_COMPLETED,
                CreatedTime = DateTime.UtcNow,
                Deleted = ModelStatusEnum.DELETED_NO,
                Title = string.Empty
            };
            TrulyCheckListItems.Add(checkListItem);
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }


    public class RepeatTimeSelection
    {
        public string Name { get; set; }
        public int RepeatTimeEnum { get; set; }
    }






    public class PrioritySelection
    {
        public string Name { get; set; }
        public int PrioritiesEnum { get; set; }
    }


}
