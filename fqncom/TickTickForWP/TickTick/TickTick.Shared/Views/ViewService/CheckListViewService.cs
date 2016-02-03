using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Views.ViewService
{
    public class CheckListViewService
    {
        private long defaultItemId = -1L;
        public String GetCompositeContent(Tasks task)
        {
            StringBuilder content = new StringBuilder();
            List<ChecklistItem> subTasks = task.ChecklistItems;
            if (subTasks.Count == 1 && subTasks[0].Id < 0
                    && string.IsNullOrEmpty(subTasks[0].Title))
            {
                if (string.IsNullOrEmpty(task.Title))
                {
                    // 如果只有一个默认产生的空item,且task title为空，则清除
                    task.ChecklistItems.Clear();
                }
                return content.ToString();
            }
            bool isFirst = true;
            foreach (var item in subTasks)
            {
                if (!isFirst)
                {
                    content.Append("\r\n");
                }
                else
                {
                    isFirst = false;
                }

                content.Append(item.Title);
            }
            return content.ToString();
        }
        public void SwitchToChecklist(Tasks task)
        {
            List<ChecklistItem> items = new List<ChecklistItem>();
            String content = task.Content;
            if (content.Contains("\n"))
            {
                if (content.Contains("\r\n"))
                {
                    //先统一替换成线上版本
                    content = content.Replace("\r\n", "\n");
                }
                //然后替换成C#版本
                content = content.Replace("\n", "\r\n");
            }
            if (!string.IsNullOrEmpty(content))
            {
                String[] arrayTitle = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 0, size = arrayTitle.Length; i < size; i++)
                {
                    if (i == 0 && string.IsNullOrEmpty(arrayTitle[i]))
                    {
                        continue;
                    }
                    String itemTitle = string.IsNullOrEmpty(arrayTitle[i]) ? "" : arrayTitle[i];
                    ChecklistItem item = new ChecklistItem();
                    //item.Id = GetDefaultItemId();// TODO 为什么要改变Id
                    item.Title = itemTitle;
                    item.TaskId = task.Id;
                    items.Add(item);
                }
            }
            else
            {
                items.Add(new ChecklistItem { Title = string.Empty, TaskId = task.Id });
            }
            task.ChecklistItems = items;
        }
        public long GetDefaultItemId()
        {
            return defaultItemId--;
        }
    }
}
