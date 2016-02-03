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
    public class LocationBll : BaseBll<Location>
    {
        //private LocationReminderService locationReminderService;
        // TODO 创建对象，暂时先这样创建
        private LocationDal LocationDal = new LocationDal();

        #region 自定义代码
        public async Task CopyLocationToCloneTask(Tasks task, int cloneTaskId, String cloneTaskSid)
        {
            if (!task.HasLocation())
            {
                return;
            }
            Location location = task.Location;
            Location clone = ObjectCopier.Clone<Location>(location);//new Location();

            clone.GeofenceId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
            clone.TaskId = cloneTaskId;
            clone.TaskSid = cloneTaskSid;
            //clone.UserId(location.getUserId());
            //clone.Address(location.getAddress());
            //clone.CreatedTime(location.getCreatedTime());
            //clone.ModifiedTime(location.getModifiedTime());
            //clone.Latitude(location.getLatitude());
            //clone.Longitude(location.getLongitude());
            //clone.Radius(location.getRadius());
            //clone.TransitionType(location.getTransitionType());
            await LocationDal.InsertLocation(clone);// TODO 在android代码中，此处有问题，保存了location而不是clone
        }
        public async Task SaveCommitResultBackToDB(Tasks task, String userId)
        {
            if (task.HasLocation())
            {
                Location location = task.Location;
                if (location.Deleted == ModelStatusEnum.DELETED_TRASH)
                {
                    await LocationDal.DeleteLocatonForever(location.Id);
                }
                else if (location.Status != ModelStatusEnum.SYNC_DONE)
                {
                    await LocationDal.UpdateLocationSyncStatus(ModelStatusEnum.SYNC_DONE, location.Id, userId);
                }
            }
        }
        public async Task SaveServerMergeToDB(LocationSyncBean locationSyncModel, String userId, Dictionary<String, int> taskIdMap)
        {
            foreach (var update in locationSyncModel.UpdateLocations)
            {
                if (taskIdMap.ContainsKey(update.TaskSid))
                {
                    int taskId = taskIdMap[update.TaskSid];
                    update.TaskId = taskId;
                    update.UserId = userId;
                    update.Status = ModelStatusEnum.SYNC_DONE;
                    await SaveServerLocation(update);
                }
            }

            foreach (var delete in locationSyncModel.DeleteLocations)
            {
                await DeleteLocationForever(delete.Id);
            }
        }
        public async Task DeleteLocationForever(long id)
        {
            await LocationDal.DeleteLocatonForever(id);
        }
        private async Task SaveServerLocation(Location location)
        {
            if (location.Id == Constants.EntityIdentifie.DEFAULT_LOCATION_ID)
            {
                if (string.IsNullOrEmpty(location.GeofenceId))
                {
                    location.GeofenceId = StringUtils.GenerateShortStringGuid();//   Utils.randomUUID32();
                }
                await LocationDal.InsertLocation(location);
            }
            else
            {
                await LocationDal.UpdateLocation(location);
            }
        }
        #endregion

        #region android代码
        //    public void saveServerMergeToDB(final LocationSyncBean locationSyncModel, final String userId,
        //        final Map<String, Long> taskIdMap) {
        //    dbHelper.doInTransaction(new Transactable<Void>() {

        //        @Override
        //        public Void doIntransaction(GTasksDBHelper dbHelper) {
        //            for (Location update : locationSyncModel.getUpdateLocations()) {
        //                if (taskIdMap.containsKey(update.getTaskSid())) {
        //                    long taskId = taskIdMap.get(update.getTaskSid());
        //                    update.setTaskId(taskId);
        //                    update.setUserId(userId);
        //                    update.setStatus(Status.SYNC_DONE);
        //                    saveServerLocation(update);
        //                }

        //            }

        //            for (Location delete : locationSyncModel.getDeleteLocations()) {
        //                deleteLocationForever(delete.getId());
        //            }
        //            return null;
        //        }
        //    });

        //}
        #endregion

        protected override void SetCurrentDal()
        {
            CurrentDal = LocationDal;
        }
    }
}
