using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Models;
using TickTick.Synchronous.Transfer;

namespace TickTick.Synchronous.Collector
{
    class AttachmentSyncCollector
    {

        #region 自定义代码
        public static async Task CollectRemoteAttachments(TasksServer serverTask, Tasks localTask,
            AttachmentSyncBean attachmentSyncBean)
        {

            // 如果server返回的附件为null时，表示server端无修改
            if (serverTask.Attachments == null)
            {
                return;
            }
            bool hasAttachRemote = HasAttachment(serverTask);
            bool hasAttachLocal = localTask.Attachments.Count > 0;

            if (hasAttachRemote && !hasAttachLocal)
            {
                //Server存在附件，Local没有，直接新增
                attachmentSyncBean.AddAllAddeds(await AttachmentTransfer.ConvertServerToLocal(serverTask.Attachments, localTask));
                localTask.HasAttachment = true;

            }
            else if (!hasAttachRemote && hasAttachLocal)
            {
                //server没有附件，Local有，判断是否要删除
                bool hasExistAttachment = false;
                foreach (var attachment in localTask.Attachments)
                {
                    if (attachment.Status == ModelStatusEnum.SYNC_DONE)
                    {
                        attachmentSyncBean.AddDeleted(attachment);
                    }
                    else
                    {
                        hasExistAttachment = true;
                    }
                }
                //及时更新Task的hasAttachment状态
                localTask.HasAttachment = hasExistAttachment;

            }
            else if (hasAttachRemote)
            {
                //Server和Local同时存在附件，需要合并
                Dictionary<String, Attachment> localAttachDic = GetLocalAttachmentDic(localTask);
                List<Attachment> serverAttachs = serverTask.Attachments;
                foreach (Attachment attachment in serverAttachs)
                {

                    if (attachment == null)
                    {
                        //TODO，List里面不应该出现NULL，是否可以删除
                        continue;
                    }

                    Attachment localAttach = localAttachDic[attachment.SId];
                    localAttachDic.Remove(attachment.SId);

                    if (localAttach == null)
                    {
                        //本地不存在对应附件，新增Server附件到Local
                        attachmentSyncBean.AddAdded(await AttachmentTransfer.ConvertServerToLocal(attachment,
                                localTask));
                    }
                }

                //找出server不存的Local已同步附件，判定为已删除
                foreach (var attachment in localAttachDic.Values)
                {
                    if (attachment.Status == ModelStatusEnum.SYNC_DONE)
                    {
                        attachmentSyncBean.AddDeleted(attachment);
                    }
                }

                //当前情况下task肯定有附件，及时更新状态
                localTask.HasAttachment = true;
            }

        }
        private static Dictionary<String, Attachment> GetLocalAttachmentDic(Tasks localTask)
        {
            Dictionary<String, Attachment> dic = new Dictionary<String, Attachment>();
            List<Attachment> localAttachs = localTask.Attachments;
            if (localAttachs == null || localAttachs.Count <= 0)
            {
                return dic;
            }
            foreach (var attachment in localAttachs)
            {
                dic.Add(attachment.SId, attachment);
            }
            return dic;
        }

        private static bool HasAttachment(TasksServer serverTask)
        {
            return serverTask.Attachments != null && serverTask.Attachments.Count > 0;
        }
        #endregion

        #region android代码
        //    public static void collectRemoteAttachments(Task serverTask, Task2 localTask,
        //        AttachmentSyncBean attachmentSyncBean) {

        //    // 如果server返回的附件为null时，表示server端无修改
        //    if (serverTask.getAttachments() == null) {
        //        return;
        //    }
        //    boolean hasAttachRemote = hasAttachment(serverTask);
        //    boolean hasAttachLocal = !localTask.getAttachments().isEmpty();

        //    if (hasAttachRemote && !hasAttachLocal) {
        //        //Server存在附件，Local没有，直接新增
        //        attachmentSyncBean.addAllAddeds(AttachmentTransfer.convertServerToLocal(
        //                serverTask.getAttachments(), localTask));
        //        localTask.setHasAttachment(true);

        //    } else if (!hasAttachRemote && hasAttachLocal) {
        //        //server没有附件，Local有，判断是否要删除
        //        boolean hasExistAttachment = false;
        //        for (Attachment attachment : localTask.getAttachments()) {
        //            if (attachment.getStatus() == Status.SYNC_DONE) {
        //                attachmentSyncBean.addDeleted(attachment);
        //            } else {
        //                hasExistAttachment = true;
        //            }
        //        }
        //        //及时更新Task的hasAttachment状态
        //        localTask.setHasAttachment(hasExistAttachment);

        //    } else if (hasAttachRemote) {
        //        //Server和Local同时存在附件，需要合并
        //        HashMap<String, Attachment> localAttachMap = getLocalAttachmentMap(localTask);
        //        List<com.ticktick.task.entity.Attachment> serverAttachs = serverTask.getAttachments();
        //        for (com.ticktick.task.entity.Attachment attachment : serverAttachs) {

        //            if (attachment == null) {
        //                //TODO，List里面不应该出现NULL，是否可以删除
        //                continue;
        //            }

        //            Attachment localAttach = localAttachMap.get(attachment.getId());
        //            localAttachMap.remove(attachment.getId());

        //            if (localAttach == null) {
        //                //本地不存在对应附件，新增Server附件到Local
        //                attachmentSyncBean.addAdded(AttachmentTransfer.convertServerToLocal(attachment,
        //                        localTask));
        //            }
        //        }

        //        //找出server不存的Local已同步附件，判定为已删除
        //        for (Attachment attachment : localAttachMap.values()) {
        //            if (attachment.getStatus() == Status.SYNC_DONE) {
        //                attachmentSyncBean.addDeleted(attachment);
        //            }
        //        }

        //        //当前情况下task肯定有附件，及时更新状态
        //        localTask.setHasAttachment(true);
        //    }

        //}
        #endregion
    }
}
