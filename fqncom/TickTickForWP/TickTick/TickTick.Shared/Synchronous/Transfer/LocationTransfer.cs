using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;

namespace TickTick.Synchronous.Transfer
{
    public class LocationTransfer
    {
        public static async Task<Location> ConvertServerToLocal(TasksServer serverTask)
        {
            return await ConvertServerToLocal(serverTask, null);
        }
        public static async Task<Location> ConvertServerToLocal(TasksServer serverTask, Location localLocation)
        {
            Location locationRemote = serverTask.Location;
            Location locationLocal = localLocation;
            if (locationLocal == null)
            {
                locationLocal = new Location();
                locationLocal.Id = Constants.EntityIdentifie.DEFAULT_LOCATION_ID;
                locationLocal.TaskSid = serverTask.Id;
            }
            locationLocal.Address = locationRemote.Address;
            locationLocal.ShortAddress = locationRemote.ShortAddress;
            Loc loc = locationRemote.Loc;
            if (loc == null)
            {
                locationLocal.Latitude = 0;
                locationLocal.Longitude = 0;
            }
            else
            {
                locationLocal.Latitude = loc.Latitude;
                locationLocal.Longitude = loc.Longitude;
            }
            if (locationRemote.Radius != null)
            {
                locationLocal.Radius = locationRemote.Radius;
            }
            if (locationRemote.TransitionType != null)
            {
                locationLocal.TransitionType = locationRemote.TransitionType;
            }
            locationLocal.Alias = locationRemote.Alias;
            if (LoggerHelper.IS_LOG_ENABLED)
            {
                await LoggerHelper.LogToAllChannels(null, locationLocal.ToString());
            }
            return locationLocal;
        }
        public static Location ConvertLocationLocalToServer(Location local)
        {
            Location remote = new Location();
            if (local.Deleted == ModelStatusEnum.DELETED_TRASH)
            {
                remote.Removed = true;
            }
            else
            {
                remote.Address = local.Address;
                remote.ShortAddress = local.ShortAddress;
                remote.Radius = local.Radius;
                remote.TransitionType = local.TransitionType;
                remote.Alias = local.Alias;
                Loc loc = new Loc();
                loc.Latitude = local.Latitude;
                loc.Longitude = local.Longitude;
                remote.Loc = loc;
            }
            return remote;
        }
    }
}
