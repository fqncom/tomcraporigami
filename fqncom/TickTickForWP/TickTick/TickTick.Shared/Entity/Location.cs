using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enums;
using Windows.Devices.Geolocation.Geofencing;

namespace TickTick.Entity
{
    public class Location
    {
        public static readonly float DEFAULT_RADIUS = 100;

        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _geofenceId;

        public string GeofenceId
        {
            get { return _geofenceId; }
            set { _geofenceId = value; }
        }
        private int _taskId;

        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
        }
        private String _taskSid;

        public String TaskSid
        {
            get { return _taskSid; }
            set { _taskSid = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private float? _radius = DEFAULT_RADIUS;

        public float? Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        private int? _transitionType = Convert.ToInt32(GeofenceState.Exited);

        public int? TransitionType
        {
            get { return _transitionType; }
            set { _transitionType = value; }
        }
        private String _address;

        public String Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private String _shortAddress;

        public String ShortAddress
        {
            get { return _shortAddress; }
            set { _shortAddress = value; }
        }
        private String _alias;

        public String Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        private int _alertStatus = ModelStatusEnum.ALERT_STATUS_NORMAL;

        public int AlertStatus
        {
            get { return _alertStatus; }
            set { _alertStatus = value; }
        }
        private DateTime _firedTime;

        public DateTime FiredTime
        {
            get { return _firedTime; }
            set { _firedTime = value; }
        }
        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private int _deleted = ModelStatusEnum.DELETED_NO;

        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
        private String _history;

        public String History
        {
            get { return _history; }
            set { _history = value; }
        }
        private bool _removed;

        public bool Removed
        {
            get { return _removed; }
            set { _removed = value; }
        }
        private Loc _loc;
        [Ignore]
        public Loc Loc
        {
            get { return _loc; }
            set { _loc = value; }
        }
        public bool IsContentChanged(Location origin)
        {

            if (!string.Equals(GeofenceId, origin.GeofenceId))
            {
                return true;
            }
            if (Latitude != origin.Latitude)
            {
                return true;
            }
            if (Longitude != origin.Longitude)
            {
                return true;
            }
            if (Radius != origin.Radius)
            {
                return true;
            }
            if (TransitionType != origin.TransitionType)
            {
                return true;
            }
            if (!string.Equals(Address, origin.Address))
            {
                return true;
            }
            if (!string.Equals(ShortAddress, origin.ShortAddress))
            {
                return true;
            }
            return !string.Equals(Alias, origin.Alias);
        }
    }
}
