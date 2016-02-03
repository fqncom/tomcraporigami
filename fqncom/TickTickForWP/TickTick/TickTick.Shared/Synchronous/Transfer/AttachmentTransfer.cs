using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Utilities.FileUtility;

namespace TickTick.Synchronous.Transfer
{
    public class AttachmentTransfer
    {
        public static async Task<List<Attachment>> ConvertServerToLocal(
            List<Attachment> attachments, Tasks localTask)
        {
            List<Attachment> localAttachs = new List<Attachment>();
            foreach (Attachment remoteAttach in attachments)
            {
                localAttachs.Add(await ConvertServerToLocal(remoteAttach, localTask));
            }
            return localAttachs;
        }
        public async static Task<Attachment> ConvertServerToLocal(Attachment remote,
            Tasks localTask)
        {
            Attachment localAttach = new Attachment();
            localAttach.SId = remote.SId;
            localAttach.UserId = localTask.UserId;
            localAttach.TaskSid = localTask.SId;
            localAttach.Description = remote.Description;
            localAttach.FileName = remote.FileName;
            string fileType = FileUtility.FileType.GetFileType(remote.FileType);
            if (fileType == null)
            {
                return null;
            }
            localAttach.FileType = fileType;
            localAttach.Status = ModelStatusEnum.SYNC_DONE;
            // TODO 可能是脏数据导致null
            localAttach.Size = remote.Size == null ? 0 : remote.Size;
            localAttach.ReferAttachmentSid = remote.RefId;
            await localAttach.InitDownloadStatus();
            //if (Log.IS_LOG_ENABLED)
            //{
            //    Log.debugSync(localAttach.toString());
            //}
            return localAttach;
        }
        public static async Task<List<Attachment>> ConvertServerToLocal(
            List<Attachment> attachments, String userId, String taskSid)
        {
            List<Attachment> localAttachs = new List<Attachment>();
            Tasks mockTask = new Tasks();
            mockTask.UserId = userId;
            mockTask.SId = taskSid;
            foreach (Attachment remoteAttach in attachments)
            {
                localAttachs.Add(await ConvertServerToLocal(remoteAttach, mockTask));
            }
            return localAttachs;
        }
        public static Attachment ConvertLocalToRemote(Attachment local)
        {
            Attachment remote = new Attachment();
            remote.Id = Convert.ToInt32(local.SId);
            remote.Description = local.Description;
            remote.FileName = local.FileName;
            remote.FileType = local.FileType;
            remote.Size = local.Size;
            remote.RefId = local.ReferAttachmentSid;
            return remote;
        }
    }
}
