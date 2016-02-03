using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Bll
{
    public class SyncStatusBll : BaseBll<SyncStatus>
    {
        private SyncStatusDal SyncStatusDal = new SyncStatusDal();


        public async Task DeleteSyncStatusForeverExceptType(String userId, String entityId, int type)
        {
            await SyncStatusDal.DeleteSyncStatusForeverExceptType(userId, entityId, type);
        }
        public async Task<HashSet<String>> GetEntityIdsByType(String userId, int type)
        {
            return await SyncStatusDal.GetEntityIdsByType(userId, type);
        }
        public async Task<Dictionary<String, String>> GetMoveFromIdDic(String userId)
        {
            return await SyncStatusDal.GetMoveFromIdDic(userId, ModelStatusEnum.SYNC_TYPE_TASK_MOVE);
        }
        public async Task<HashSet<String>> GetContentChangeEntityIds(String userId)
        {
            return await SyncStatusDal.GetEntityIdsByType(userId, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
        }
        public async Task<HashSet<String>> GetAssignEntityIds(String userId)
        {
            return await SyncStatusDal.GetEntityIdsByType(userId, ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN);
        }
        public async Task DeleteSyncStatus(String userId, String entityId, int type)
        {
            await SyncStatusDal.DeleteSyncStatusForever(userId, entityId, type);
        }
        public async Task<bool> UpdateMoveFromId(String taskSid, String userId, String moveFromId)
        {
            return await SyncStatusDal.UpdateMoveFromId(taskSid, userId, moveFromId);
        }
        public async Task DeleteSyncStatusPhysical(String userId, String entityId)
        {
            await SyncStatusDal.DeleteSyncStatusPhysical(userId, entityId);
        }
        public async Task AddSyncStatus(Tasks task, int type)
        {
            await AddSyncStatus(task, type, null);
        }

        public async Task AddSyncStatus(Tasks task, int type, String fromProjectId)
        {
            //Local Mode下，无需添加同步状态位
            if (string.Equals(User.LOCAL_MODE_ID, task.UserId))
            {
                return;
            }
            if (type == ModelStatusEnum.SYNC_TYPE_TASK_CREATE)
            {
                //CREATE操作，只需增加同步状态SYNC_TYPE_TASK_CREATE，同时新增的操作默认只执行一次，不需要判断状态是否已存在
                //Log.debugSyncStatus("新增CREATE, " + task.toSyncString());
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_CREATE));
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT)
            {
                await AddSyncStatusContent(task, type);
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_ORDER)
            {
                await AddSyncStatusOrder(task, type);
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_MOVE)
            {
                await AddSyncStatusMove(task, type, fromProjectId);
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN)
            {
                await AddSyncStatusAssign(task, type);
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_TRASH)
            {
                await AddSyncStatusTrash(task, type);
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER)
            {
                //Log.debugSyncStatus("新增DELETE_FOREVER, " + task.toSyncString());
                //DELETE_FOREVER操作，清除所以其他同步状态
                // TODO 问题，未添加delete_forever状态
                await SyncStatusDal.DeleteSyncStatusForeverExceptType(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER);
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER));
            }
            else if (type == ModelStatusEnum.SYNC_TYPE_TASK_RESTORE)
            {
                await AddSyncStatusRestore(task, type, fromProjectId);
            }
        }
        private async Task AddSyncStatusRestore(Tasks task, int type, String fromProjectId)
        {
            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type, fromProjectId));
                //Log.debugSyncStatus("新增RESTORE<没其他状态>, " + task.toSyncString());
                return;
            }

            //CREATE状态应该已经在Trash时被删除
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                //Log.debugSyncStatus("新增RESTORE时, 删除CREATE！！！");
            }

            //ASSIGN状态应该已经在Trash时被删除
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN);
                //Log.debugSyncStatus("新增RESTORE时, 删除ASSIGN！！！");
            }

            //TRASH状态存在时，RESTORE变相为MOVE操作，TRASH被删除，RESTORE不新增
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_TRASH))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_TRASH);
                //Log.debugSyncStatus("新增RESTORE时, 删除TRASH！！！");
            }

            //ORDER会被RESTORE变相的MOVE操作替换
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ORDER))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ORDER);
                //Log.debugSyncStatus("新增RESTORE时, 删除ORDER！！！");
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_TRASH) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_RESTORE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER))
            {
                //已存在TRASH,DELETE_FOREVER, RESTORE状态，不用新增
                //Log.debugSyncStatus(
                //        "新增TRASH<存在TRASH,DELETE_FOREVER, RESTORE> < " + syncStatusMap.keySet()
                //                + " >, 不用新增");
                return;
            }
            //其他状态应该都是无效的，不影响新增RESTORE状态
            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type, fromProjectId));
            //Log.debugSyncStatus("新增RESTORE < " + syncStatusMap.keySet() + " >, " + task.toSyncString());
        }
        private async Task AddSyncStatusTrash(Tasks task, int type)
        {
            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
                //Log.debugSyncStatus("新增TRASH<没其他状态>, " + task.toSyncString());
                return;
            }

            //TRASH后，CREATE失效
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                //Log.debugSyncStatus("新增TRASH时, 删除CREATE！！！");
            }

            //TRASH后，ASSIGN失效
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN);
                //Log.debugSyncStatus("新增TRASH时, 删除ASSIGN！！！");
            }

            //TRASH后，RESTORE失效
            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_RESTORE))
            {
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_RESTORE);
                //Log.debugSyncStatus("新增TRASH时, 删除RESTORE！！！");
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_TRASH) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_RESTORE))
            {
                //已存在CREATE,TRASH,DELETE_FOREVER, RESTORE，不用新增
                //Log.debugSyncStatus(
                //        "新增TRASH<存在CREATE,TRASH,DELETE_FOREVER, RESTORE> < " + syncStatusDic.keySet()
                //                + " >, 不用新增");
                return;
            }

            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
            //Log.debugSyncStatus("新增TRASH < " + syncStatusDic.keySet() + " >, " + task.toSyncString());
        }
        private async Task AddSyncStatusAssign(Tasks task, int type)
        {

            if (task.Assignee == Constants.Removed.ASSIGNEE)
            {
                //清除指派目前不需要增加同步状态
                return;
            }

            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
                //Log.debugSyncStatus("新增ASSIGN<没其他状态>, " + task.toSyncString());
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_TRASH) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER))
            {
                //已存在ASSIGN,TRASH,DELETE_FOREVER状态，不用新增
                //Log.debugSyncStatus(
                //        "新增ASSIGN<存在ASSIGN,TRASH,DELETE_FOREVER> < " + syncStatusMap.keySet()
                //                + " >, 不用新增");
                return;
            }

            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
            //Log.debugSyncStatus("新增ASSIGN < " + syncStatusMap.keySet() + " >, " + task.toSyncString());
        }
        private async Task AddSyncStatusMove(Tasks task, int type, String moveFromId)
        {
            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type, moveFromId));
                //Log.debugSyncStatus("新增MOVE<没其他状态>, " + task.toSyncString());
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN))
            {
                //MOVE时，ASSIGN无效
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN);
                //Log.debugSyncStatus("新增MOVE时, 删除ASSIGN！！！");
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_MOVE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_RESTORE))
            {
                //已存在CREATE,MOVE,DELETE_FOREVER状态，不用新增
                //Log.debugSyncStatus("新增MOVE<存在CREATE,MOVE,DELETE_FOREVER> < " + syncStatusDic.keySet()
                //        + " >, 不用新增");
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ORDER))
            {
                //MOVE操作中，包含了ORDER
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ORDER);
                //Log.debugSyncStatus("新增MOVE时, 删除ORDER！！！");
            }

            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type, moveFromId));
            //Log.debugSyncStatus("新增MOVE < " + syncStatusDic.keySet() + " >, " + task.toSyncString());
        }
        private async Task AddSyncStatusOrder(Tasks task, int type)
        {
            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
                //Log.debugSyncStatus("新增ORDER<没其他状态>, " + task.toSyncString());
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CONTENT) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ORDER) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_MOVE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER))
            {
                //已存在CREATE,CONTENT,ORDER,MOVE,DELETE_FOREVER状态，不用新增
                //Log.debugSyncStatus(
                //        "新增ORDER<存在CREATE,CONTENT,ORDER,MOVE,DELETE_FOREVER> < " + syncStatusMap
                //                .keySet() + " >, 不用新增");
                return;
            }

            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
            //Log.debugSyncStatus("新增ORDER < " + syncStatusMap.keySet() + " >, " + task.toSyncString());
        }

        private async Task AddSyncStatusContent(Tasks task, int type)
        {
            Dictionary<int, SyncStatus> syncStatusDic = await SyncStatusDal.GetSyncStatusDic(task.UserId, task.SId);

            if (syncStatusDic.Count <= 0)
            {
                //没有已存在状态，直接新增
                await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
                //Log.debugSyncStatus("新增CONTENT<没其他状态>, " + task.toSyncString());
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CREATE) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_CONTENT) || syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_DELETE_FOREVER))
            {
                //已存在CREATE,CONTENT,DELETE_FOREVER状态，不用新增
                //Log.debugSyncStatus(
                //        "新增CONTENT<存在CREATE,CONTENT或DELETE_FOREVER> < " + syncStatusDic.keySet()
                //                + " >, 不用新增");
                return;
            }

            if (syncStatusDic.ContainsKey(ModelStatusEnum.SYNC_TYPE_TASK_ORDER))
            {
                //CONTENT操作已包含ORDER
                await SyncStatusDal.DeleteSyncStatusForever(task.UserId, task.SId, ModelStatusEnum.SYNC_TYPE_TASK_ORDER);
                //Log.debugSyncStatus("新增CONTENT时, 删除ORDER！！！");
            }
            await SyncStatusDal.AddSyncStatus(new SyncStatus(task.UserId, task.SId, type));
            //Log.debugSyncStatus("新增CONTENT < " + syncStatusDic.keySet() + " >, " + task.toSyncString());
        }

        protected override void SetCurrentDal()
        {
            CurrentDal = SyncStatusDal;
        }

        public async Task<HashSet<String>> GetSortOrderChangeEntityIds(String userId)
        {
            return await SyncStatusDal.GetEntityIdsByType(userId, ModelStatusEnum.SYNC_TYPE_TASK_ORDER);
        }

        public async Task<HashSet<String>> GetMoveToTrashEntityIds(String userId)
        {
            return await SyncStatusDal.GetEntityIdsByType(userId, ModelStatusEnum.SYNC_TYPE_TASK_TRASH);
        }
    }
}
