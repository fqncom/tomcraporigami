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
    public class LocationSyncCollector
    {
        public static async Task CollectRemoteLocations(TasksServer serverTask, Tasks localTask,LocationSyncBean locationSyncBean)
        {
            Location local = localTask.Location;
            if (HasLocation(serverTask) && local == null)
            {
                locationSyncBean.AddUpdateLocation(await LocationTransfer.ConvertServerToLocal(serverTask));
            }
            else if (HasLocation(serverTask) && local != null)
            {
                if (local.Status == ModelStatusEnum.SYNC_DONE)
                {
                    locationSyncBean.AddUpdateLocation(await LocationTransfer.ConvertServerToLocal(serverTask, local));
                }
            }
            else if (!HasLocation(serverTask) && local != null)
            {
                if (local.Status == ModelStatusEnum.SYNC_DONE)
                {
                    locationSyncBean.AddDeleteLocation(local);
                }
            }
        }
        private static bool HasLocation(TasksServer serverTask)
        {
            return serverTask.Location != null;
        }
    }
}
