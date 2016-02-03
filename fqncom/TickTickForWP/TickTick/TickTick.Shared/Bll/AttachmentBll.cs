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
    public class AttachmentBll : BaseBll<Attachment>
    {
        private AttachmentDal AttachmentDal = new AttachmentDal();
        private ReferAttachmentDal ReferAttachmentDal = new ReferAttachmentDal();

        #region 自定义代码
        
        public async Task CopyAttachmentForCloneTask(Tasks task, int cloneTaskId, String cloneTaskSid)
        {
            if (!task.HasAttachment)
            {
                return;
            }
            List<Attachment> attachments = task.Attachments;
            if (attachments.Count <= 0)
            {
                attachments = await GetAllAttachmentByTaskId(task.Id, task.UserId);
            }
            List<Attachment> cloneAttachments = new List<Attachment>();
            foreach (var attachment in attachments)
            {
                Attachment clone = ObjectCopier.Clone<Attachment>(attachment);
                clone.SId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
                clone.UserId = task.UserId;
                clone.TaskId = cloneTaskId;
                clone.TaskSid = cloneTaskSid;
                //clone.LocalPath(attachment.getLocalPath());
                //clone.Size(attachment.getSize());
                //clone.FileName(attachment.getFileName());
                //clone.FileType(attachment.getFileType());
                //clone.Description(attachment.getDescription());
                //clone.OtherData(attachment.getOtherData());
                // TODO clone的任务文件同步状态为完成，当原附件删除时可能更改状态，界面显示附件同步状态需要另外处理
                clone.UpDown = ModelStatusEnum.UP_DOWN_DONE;
                //clone.setSyncErrorCode(attachment.getSyncErrorCode());
                //if (TextUtils.isEmpty(attachment.getReferAttachmentSid())) {
                //    clone.setReferAttachmentSid(attachment.getSid());
                //} else {
                //    //clone.setReferAttachmentSid(attachment.getReferAttachmentSid());
                //}
                cloneAttachments.Add(clone);
            }

            if (cloneAttachments.Count <= 0)
            {
                return;
            }
            //dbHelper.doInTransaction(new Transactable<Void>() {

            //    @Override
            //    public Void doIntransaction(GTasksDBHelper dbHelper) {
            foreach (var attachment in cloneAttachments)
            {
                ReferAttachment referAttachment = new ReferAttachment();
                referAttachment.UserId = attachment.UserId;
                referAttachment.RefAttachmentSid = attachment.ReferAttachmentSid;
                referAttachment.AttachmentSid = attachment.SId;
                await Task.WhenAll(ReferAttachmentDal.CreateReferAttachment(referAttachment), AttachmentDal.InsertAttachment(attachment));
                //await ReferAttachmentDal.CreateReferAttachment(referAttachment);
                //await AttachmentDal.InsertAttachment(attachment);
            }
            //        return null;
            //    }
            //});

        }
        public async Task<List<Attachment>> GetAllAttachmentByTaskId(long taskId, String userId)
        {
            return await AttachmentDal.GetAllAttachmentByTaskId(taskId, userId);
        }
        public async Task SaveCommitResultBackToDB(Tasks task)
        {
            List<Attachment> attachments = task.Attachments;
            if (attachments == null || attachments.Count <= 0)
            {
                return;
            }
            foreach (Attachment attachment in attachments)
            {
                if (attachment.Deleted == ModelStatusEnum.DELETED_TRASH)
                {
                    await AttachmentDal.DeleteAttachmentForever(attachment.Id);
                }
                else if (attachment.Status == ModelStatusEnum.SYNC_NEW)
                {
                    await AttachmentDal.UpdateSyncStatus(ModelStatusEnum.SYNC_DONE, attachment.Id);
                }
                else if (attachment.Status == ModelStatusEnum.SYNC_UPDATE)
                {
                    await AttachmentDal.UpdateSyncStatus(ModelStatusEnum.SYNC_DONE, attachment.Id);
                }
            }
        }
        public async Task<List<Attachment>> GetAllAttachment(String userId, bool withDeleted)
        {
            return await AttachmentDal.GetAllAttachment(userId, withDeleted);
        }
        public async Task DeleteAttachmentForeverByTaskId(int taskId)
        {
            await AttachmentDal.DeleteAttachmentForeverByTaskId(taskId);
        }
        public async Task SaveServerMergeToDB(AttachmentSyncBean attachmentSyncBean, String userId, Dictionary<String, int> taskIdDic)
        {
            List<Attachment> deleteds = attachmentSyncBean.Deleted;
            foreach (var deleted in deleteds)
            {
                await AttachmentDal.DeleteAttachmentForever(deleted.Id);
            }
            List<Attachment> addeds = attachmentSyncBean.Added;
            foreach (var added in addeds)
            {
                int taskId = taskIdDic[added.TaskSid];
                if (taskId != null)
                {
                    added.TaskId = taskId;
                    await AttachmentDal.InsertAttachment(added);
                }
            }
            List<Attachment> updateds = attachmentSyncBean.Updated;
            foreach (Attachment updated in updateds)
            {
                int taskId = taskIdDic[updated.TaskSid];
                if (taskId != null)
                {
                    updated.TaskId = taskId;
                    await AttachmentDal.UpdateAttachment(updated);
                }
            }
        }
        #endregion

        #region android代码

        //    public void saveServerMergeToDB(final AttachmentSyncBean attachmentSyncBean, String userId,
        //        final Map<String, Long> taskIdMap) {
        //    dbHelper.doInTransaction(new Transactable<Boolean>() {

        //        @Override
        //        public Boolean doIntransaction(GTasksDBHelper dbHelper) {
        //            List<Attachment> deleteds = attachmentSyncBean.getDeleted();
        //            for (Attachment deleted : deleteds) {
        //                attachmentDao.deleteAttachmentForever(deleted.getId());
        //            }
        //            List<Attachment> addeds = attachmentSyncBean.getAdded();
        //            for (Attachment added : addeds) {
        //                Long taskId = taskIdMap.get(added.getTaskSid());
        //                if (taskId != null) {
        //                    added.setTaskId(taskId);
        //                    attachmentDao.insertAttachment(added);
        //                }
        //            }
        //            List<Attachment> updateds = attachmentSyncBean.getUpdated();
        //            for (Attachment updated : updateds) {
        //                Long taskId = taskIdMap.get(updated.getTaskSid());
        //                if (taskId != null) {
        //                    updated.setTaskId(taskId);
        //                    attachmentDao.updateAttachment(updated);
        //                }
        //            }
        //            return null;
        //        }
        //    });
        //}
        //public ArrayList<Attachment> getAllAttachment(String userId, boolean withDeleted)
        //{
        //    return attachmentDao.getAllAttachment(userId, withDeleted);
        //}
        #endregion

        protected override void SetCurrentDal()
        {
            CurrentDal = AttachmentDal;
        }
    }
}
