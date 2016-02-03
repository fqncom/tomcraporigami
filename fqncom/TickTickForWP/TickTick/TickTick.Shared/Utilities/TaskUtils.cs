using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Models;

namespace TickTick.Utilities
{
    public class TaskUtils
    {

        #region 自定义代码
        public static bool IsDuedateChanged(Tasks task1, Tasks task2)
        {
            if (task1.DueDate == null && task2.DueDate == null)
            {
                return false;
            }
            if (task1.DueDate != null && task2.DueDate != null)
            {
                // TODO 时间插件
                //DateTime taskDueDateClean = DateUtils.clearSecondOfDay(task1.getDueDate());
                //DateTime duedateClean = DateUtils.clearSecondOfDay(task2.getDueDate());
                //return taskDueDateClean.compareTo(duedateClean) != 0;
            }
            return true;
        }
        public static void MergeTask(Tasks original, Tasks delta, Tasks revised)
        {
            try
            {
                revised.TaskStatus = GetRevisedStatus(original.TaskStatus, delta.TaskStatus, revised.TaskStatus);
                revised.Title = GetRevisedTextByDiff(original.Title, delta.Title, revised.Title);
                revised.Content = GetRevisedTextByDiff(original.Content, delta.Content, revised.Content);
                revised.SortOrder = GetRevisedSortOrder(original.SortOrder, delta.SortOrder, revised.SortOrder);
                revised.SnoozeRemindTime = GetRevisedDueDate(original.SnoozeRemindTime, delta.SnoozeRemindTime, revised.SnoozeRemindTime);

                bool dueDateReviseChange = !IsSameDate(original.DueDate, revised.DueDate);
                if (dueDateReviseChange)
                {
                    revised.SnoozeRemindTime = dueDateReviseChange ? null : delta.SnoozeRemindTime;

                }
                revised.DueDate = GetRevisedDueDate(original.DueDate, delta.DueDate, revised.DueDate);
                revised.CompletedTime = GetRevisedDueDate(original.CompletedTime, delta.CompletedTime, revised.CompletedTime);
                revised.Reminder = GetRevisedText(original.Reminder, delta.Reminder, revised.Reminder);
                revised.RepeatFlag = GetRevisedText(original.RepeatFlag, delta.RepeatFlag, revised.RepeatFlag);
                revised.Priority = GetRevisedPriority(original.Priority, delta.Priority, revised.Priority);

                if (revised.DueDate == null)
                {
                    revised.IsAllDay = false;
                }
                else
                {
                    revised.IsAllDay = GetRevisedIsAllDay(original.IsAllDay, delta.IsAllDay, revised.IsAllDay);
                }

                MergeChecklistItems(original, delta, revised);

                MergeReminders(original, delta, revised);
            }

            catch (Exception e)
            {
                //Log.e(TAG, e.getMessage(), e);
            }
        }
        private static void MergeReminders(Tasks original, Tasks delta, Tasks revised)
        {
            Dictionary<String, TaskReminder> revisedReminderMap = new Dictionary<String, TaskReminder>();
            foreach (TaskReminder reminder in revised.Reminders)
            {
                revisedReminderMap.Add(reminder.Sid, reminder);
            }
            Dictionary<String, TaskReminder> originalReminderMap = new Dictionary<String, TaskReminder>();
            foreach (TaskReminder reminder in original.Reminders)
            {
                originalReminderMap.Add(reminder.Sid, reminder);
            }
            Dictionary<String, TaskReminder> deltaReminderMap = new Dictionary<String, TaskReminder>();
            foreach (TaskReminder deltaReminder in delta.Reminders)
            {
                deltaReminderMap.Add(deltaReminder.Sid, deltaReminder);

                TaskReminder revisedReminder = revisedReminderMap[deltaReminder.Sid];
                TaskReminder originalReminder = originalReminderMap[deltaReminder.Sid];

                if (revisedReminder == null)
                {
                    if (originalReminder == null)
                    {
                        // 新增revised版本
                        revisedReminderMap.Add(deltaReminder.Sid, deltaReminder);
                    }
                    else
                    {
                        if (originalReminder.Duration.EqualValue(deltaReminder.Duration))
                        {
                            // 没有对应的revise版本，并且Delta版本没有修改，指定revised版本被删除
                        }
                        else
                        {
                            // 有修改，revised不被判定为删除
                            revisedReminderMap.Add(deltaReminder.Sid, deltaReminder);
                        }
                    }
                }
                else
                {
                    //revise和delta版本都存在的情况下，进行merge操作
                    if (originalReminder != null)
                    {
                        revisedReminder.Duration = GetRevisedDuration(originalReminder.Duration, deltaReminder.Duration, revisedReminder.Duration);
                    }
                }
            }

            foreach (TaskReminder reminder in originalReminderMap.Values)
            {
                //delta版本已删除，只有当revise版本没有修改时，才指定revise被删除
                if (!deltaReminderMap.ContainsKey(reminder.Sid))
                {
                    TaskReminder revisedReminder = revisedReminderMap[reminder.Sid];
                    if (revisedReminder != null && reminder.Duration.EqualValue(revisedReminder.Duration))
                    {
                        revisedReminderMap.Remove(reminder.Sid);
                    }
                }
            }

            revised.Reminders = FilterDuplicateReminders(revisedReminderMap.Values);
        }
        private static List<TaskReminder> FilterDuplicateReminders(ICollection<TaskReminder> reminders)
        {
            List<TaskReminder> filteredList = new List<TaskReminder>();
            List<long> existDurationMins = new List<long>();
            foreach (TaskReminder reminder in reminders)
            {
                long durationMins = reminder.Duration.ToMillis();
                if (!existDurationMins.Contains(durationMins))
                {
                    filteredList.Add(reminder);
                    existDurationMins.Add(durationMins);
                }
            }
            return filteredList;
        }
        private static TickTickDuration GetRevisedDuration(TickTickDuration original, TickTickDuration delta, TickTickDuration revised)
        {
            if (original.Equals(delta))
            {
                return revised;
            }
            else if (original.Equals(revised))
            {
                return delta;
            }
            return revised;
        }
        private static void MergeChecklistItems(Tasks original, Tasks delta, Tasks revised)
        {
            Dictionary<String, ChecklistItem> revisedItems = new Dictionary<String, ChecklistItem>();
            foreach (var item in revised.ChecklistItems)
            {
                revisedItems.Add(item.SId, item);
            }

            Dictionary<String, ChecklistItem> originalItems = new Dictionary<String, ChecklistItem>();
            foreach (var item in original.ChecklistItems)
            {
                originalItems.Add(item.SId, item);
            }

            Dictionary<String, ChecklistItem> deltaItems = new Dictionary<String, ChecklistItem>();
            ChecklistItem revisedItem, originalItem;
            foreach (var deltaItem in delta.ChecklistItems)
            {
                deltaItems.Add(deltaItem.SId, deltaItem);
                //revise和delta版本都存在的情况下，正常merge
                if (revisedItems.ContainsKey(deltaItem.SId))
                {
                    revisedItem = revisedItems[deltaItem.SId];
                    originalItem = originalItems[deltaItem.SId];
                    if (originalItem != null)
                    {
                        // originalItem不应该为NULL，未知异常
                        revisedItem.Checked = GetRevisedStatus(originalItem.Checked, deltaItem.Checked, revisedItem.Checked);
                        revisedItem.Title = GetRevisedTextByDiff(originalItem.Title, deltaItem.Title, revisedItem.Title);
                        revisedItem.SortOrder = GetRevisedSortOrder(originalItem.SortOrder, deltaItem.SortOrder, revisedItem.SortOrder);
                    }
                }
                else
                {
                    if (originalItems.ContainsKey(deltaItem.SId))
                    {
                        originalItem = originalItems[deltaItem.SId];
                        if (string.Equals(deltaItem.Title, originalItem.Title) && deltaItem.Checked == originalItem.Checked)
                        {
                            // 没有对应的revise版本，并且Delta版本没有修改，指定revised版本被删除
                        }
                        else
                        {
                            // 有修改，revised不被判定为删除。
                            revisedItems.Add(deltaItem.SId, deltaItem);
                        }
                    }
                    else
                    {
                        // 新增revised版本
                        revisedItems.Add(deltaItem.SId, deltaItem);
                    }
                }
            }

            foreach (ChecklistItem oitem in originalItems.Values)
            {
                //delta版本已删除，只有当revise版本没有修改时，才指定revise被删除
                if (!deltaItems.ContainsKey(oitem.SId))
                {
                    revisedItem = revisedItems[oitem.SId];
                    if (revisedItem != null && string.Equals(revisedItem.Title, oitem.Title) && revisedItem.Checked == oitem.Checked)
                    {
                        revisedItems.Remove(oitem.SId);
                    }
                }
            }

            revised.ChecklistItems = new List<ChecklistItem>(revisedItems.Values);

        }
        private static bool GetRevisedIsAllDay(bool original, bool delta, bool revised)
        {
            if (original == delta)
            {
                return revised;
            }
            else if (original == revised)
            {
                return delta;
            }
            return revised;
        }
        private static int GetRevisedPriority(int original, int delta, int revised)
        {
            if (original == delta)
            {
                return revised;
            }
            else if (original == revised)
            {
                return delta;
            }
            return revised;
        }
        private static String GetRevisedText(String original, String delta, String revised)
        {
            if (string.Equals(original, delta))
            {
                return revised;
            }
            else if (string.Equals(original, revised))
            {
                return delta;
            }
            return revised;
        }
        private static DateTime? GetRevisedDueDate(DateTime? original, DateTime? delta, DateTime? revised)
        {
            if (IsSameDate(original, delta))
            {
                return revised;
            }
            else if (IsSameDate(original, revised))
            {
                return delta;
            }
            return revised;
        }
        private static bool IsSameDate(DateTime? d1, DateTime? d2)
        {
            if (d1 == null)
            {
                if (d2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (d2 == null)
                {
                    return false;
                }
                else
                {
                    return d1.Value.CompareTo(d2.Value) == 0;
                }
            }
        }
        private static long? GetRevisedSortOrder(long? original, long? delta, long? revised)
        {
            long? originalValue = original == null ? long.MinValue : original;
            long? deltaValue = delta == null ? long.MinValue : delta;
            long? revisedValue = revised == null ? long.MinValue : revised;
            if (originalValue == deltaValue)
            {
                return revisedValue;
            }
            else if (originalValue == revisedValue)
            {
                return deltaValue;
            }
            return revisedValue;
        }
        private static String GetRevisedTextByDiff(String original, String delta, String revised)
        {
            if (string.Equals(delta, revised) || string.Equals(delta, original))
            {
                return revised;
            }
            // TODO 由于不知道这里的业务逻辑，所以暂时不做处理  ,后续.找到对应的类
            diff_match_patch diff = new diff_match_patch();
            return (String)diff.patch_apply(diff.patch_make(original, delta), revised)[0];
        }
        private static int GetRevisedStatus(int original, int delta, int revised)
        {
            if (original == Constants.TaskStatus.UNCOMPLETED)
            {
                return Max(delta, revised);
            }
            else if (original == Constants.TaskStatus.COMPLETED)
            {
                if (delta == Constants.TaskStatus.UNCOMPLETED || revised == Constants.TaskStatus.UNCOMPLETED)
                {
                    return Constants.TaskStatus.UNCOMPLETED;
                }
                else
                {
                    return Max(delta, revised);
                }
            }
            else
            {
                return Min(delta, revised);
            }
        }
        private static int Min(int a, int b)
        {
            if (b < a)
            {
                return b;
            }
            return a;
        }
        private static int Max(int a, int b)
        {

            if (b > a)
            {
                return b;
            }
            return a;
        }
        #endregion
        #region android代码
        //   public static void mergeTask(Task2 original, Task2 delta, Task2 revised) {
        //    try {
        //        revised.setTaskStatus(getRevisedStatus(original.getTaskStatus(), delta.getTaskStatus(),
        //                revised.getTaskStatus()));
        //        revised.setTitle(getRevisedTextByDiff(original.getTitle(), delta.getTitle(),
        //                revised.getTitle()));
        //        revised.setContent(getRevisedTextByDiff(original.getContent(), delta.getContent(),
        //                revised.getContent()));
        //        revised.setSortOrder(getRevisedSortOrder(original.getSortOrder(), delta.getSortOrder(),
        //                revised.getSortOrder()));
        //        revised.setDueDate(getRevisedDueDate(original.getDueDate(), delta.getDueDate(),
        //                revised.getDueDate()));
        //        revised.setReminderTime(getRevisedDueDate(original.getReminderTime(),
        //                delta.getReminderTime(), revised.getReminderTime()));
        //        revised.setCompletedTime(getRevisedDueDate(original.getCompletedTime(),
        //                delta.getCompletedTime(), revised.getCompletedTime()));
        //        revised.setReminder(getRevisedText(original.getReminder(), delta.getReminder(),
        //                revised.getReminder()));
        //        revised.setRepeatFlag(getRevisedText(original.getRepeatFlag(), delta.getRepeatFlag(),
        //                revised.getRepeatFlag()));
        //        revised.setPriority(getRevisedPriority(original.getPriority(), delta.getPriority(),
        //                revised.getPriority()));

        //        HashMap<String, ChecklistItem> revisedItems = new HashMap<String, ChecklistItem>();
        //        for (ChecklistItem item : revised.getChecklistItems()) {
        //            revisedItems.put(item.getSid(), item);
        //        }

        //        HashMap<String, ChecklistItem> originalItems = new HashMap<String, ChecklistItem>();
        //        for (ChecklistItem item : original.getChecklistItems()) {
        //            originalItems.put(item.getSid(), item);
        //        }

        //        HashMap<String, ChecklistItem> deltaItems = new HashMap<String, ChecklistItem>();
        //        ChecklistItem revisedItem, originalItem;
        //        for (ChecklistItem deltaItem : delta.getChecklistItems()) {
        //            deltaItems.put(deltaItem.getSid(), deltaItem);
        //            if (revisedItems.containsKey(deltaItem.getSid())) {
        //                revisedItem = revisedItems.get(deltaItem.getSid());
        //                originalItem = originalItems.get(deltaItem.getSid());
        //                if (originalItem != null) {
        //                    // originalItem不应该为NULL，未知异常
        //                    revisedItem.setChecked(getRevisedStatus(originalItem.getChecked(),
        //                            deltaItem.getChecked(), revisedItem.getChecked()));
        //                    revisedItem.setTitle(getRevisedTextByDiff(originalItem.getTitle(),
        //                            deltaItem.getTitle(), revisedItem.getTitle()));
        //                    revisedItem.setSortOrder(getRevisedSortOrder(originalItem.getSortOrder(),
        //                            deltaItem.getSortOrder(), revisedItem.getSortOrder()));
        //                }
        //            } else {
        //                if (originalItems.containsKey(deltaItem.getSid())) {
        //                    originalItem = originalItems.get(deltaItem.getSid());
        //                    if (TextUtils.equals(deltaItem.getTitle(), originalItem.getTitle())
        //                            && deltaItem.getChecked() == originalItem.getChecked()) {
        //                        // revised中被删除了
        //                    } else {
        //                        // 有修改，不被判定为删除了。
        //                        revisedItems.put(deltaItem.getSid(), deltaItem);
        //                    }
        //                } else {
        //                    // 新增
        //                    revisedItems.put(deltaItem.getSid(), deltaItem);
        //                }
        //            }
        //        }

        //        for (ChecklistItem oitem : originalItems.values()) {
        //            if (!deltaItems.containsKey(oitem.getSid())) {
        //                revisedItem = revisedItems.get(oitem.getSid());
        //                if (revisedItem != null
        //                        && TextUtils.equals(revisedItem.getTitle(), oitem.getTitle())
        //                        && revisedItem.getChecked() == oitem.getChecked()) {
        //                    revisedItems.remove(oitem.getSid());
        //                }
        //            }
        //        }

        //        revised.setChecklistItems(new ArrayList<ChecklistItem>(revisedItems.values()));

        //    } catch (Exception e) {
        //        Log.e(TAG, e.getMessage(), e);
        //    }
        //}
        #endregion
    }
}
