using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Utilities;

namespace TickTick.Bll
{
    public class ChecklistItemBll : BaseBll<ChecklistItem>
    {
        private ChecklistItemDal ChecklistItemDal = new ChecklistItemDal();

        public async Task CopyChecklistItemToCloneTask(Tasks task, int cloneTaskId, String cloneTaskSid)
        {
            if (!task.IsChecklistMode())
            {
                return;
            }
            List<ChecklistItem> cloneItems = new List<ChecklistItem>();
            // 当前没有上传归档子任务的接口，客户端删除的子任务不能同步备案到server
            List<ChecklistItem> checklistItems = await ChecklistItemDal.GetChecklistItemsByTaskId(task.Id, task.UserId, false);
            foreach (var item in checklistItems)
            {
                cloneItems.Add(CloneChecklistItem(item, cloneTaskId, cloneTaskSid));
            }
            foreach (var item in cloneItems)
            {
                await ChecklistItemDal.CreateChecklistItem(item);
            }
            await ChecklistItemDal.UpdateCheckStatusByTask(task.UserId, task.Id, ModelStatusEnum.NOT_COMPLETED);
            //dbHelper.doInTransaction(new Transactable<Boolean>() {

            //    @Override
            //    public Boolean doIntransaction(GTasksDBHelper dbHelper) {
            //        for (ChecklistItem item : cloneItems) {
            //            checklistItemDao.createChecklistItem(item);
            //        }
            //        checklistItemDao.updateCheckStatusByTask(task.getUserId(), task.getId(),
            //                Status.NOT_COMPLETED);
            //        return true;
            //    }
            //});

        }
        private ChecklistItem CloneChecklistItem(ChecklistItem item, int taskId, String taskSid)
        {
            ChecklistItem cloneItem = ObjectCopier.Clone<ChecklistItem>(item);
            cloneItem.SId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
            cloneItem.TaskId = taskId;
            cloneItem.TaskSid = taskSid;
            cloneItem.Status = ModelStatusEnum.SYNC_NEW;
            return cloneItem;
        }
        protected override void SetCurrentDal()
        {
            CurrentDal = ChecklistItemDal;
        }

        public async Task SaveCommitResultBackToDB(Tasks task, String etag, String userId)
        {
            if (task.IsChecklistMode())
            {
                await ChecklistItemDal.UpdateEtagToDbByTask(userId, task.SId, etag);
            }
        }
    }
}
