using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Synchronous.Transfer
{
    public class ChecklistItemTransfer
    {
        public static ChecklistItem ConvertServerToLocal(ChecklistItem serverItem)
        {
            ChecklistItem item = new ChecklistItem();
            // server status : NORMAL = 0; DONE = 1; ARCHIVED = 2;
            item.Checked = serverItem.Status;
            item.Title = serverItem.Title;
            item.SortOrder = serverItem.SortOrder;
            item.SId = serverItem.SId;
            return item;
        }
        public static List<ChecklistItem> ConvertCheckListItemLocalToServer(List<ChecklistItem> checklistItems)
        {
            List<ChecklistItem> serverItems = new List<ChecklistItem>();
            foreach (ChecklistItem localItem in checklistItems)
            {
                ChecklistItem batchItem = new ChecklistItem();
                if (!localItem.IsDeletedForever())
                {
                    //batchItem.Id = localItem.SId; // TODO 此处可能是个坑  后续：使用json转换工具之后，不必转换sid到id
                    batchItem.SortOrder = localItem.SortOrder;
                    batchItem.Title = localItem.Title;
                    batchItem.Status = localItem.IsChecked ? Constants.CompletedStatus.DONE : Constants.CompletedStatus.NORMAL;
                    serverItems.Add(batchItem);
                }
            }
            return serverItems;
        }
    }
}
