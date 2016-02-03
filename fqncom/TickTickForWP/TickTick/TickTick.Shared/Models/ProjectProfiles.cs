using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;

namespace TickTick.Models
{
    public class ProjectProfiles
    {
        private int _dataId;
        [PrimaryKey]
        [AutoIncrement]
        public int DataId
        {
            get { return _dataId; }
            set { _dataId = value; }
        }

        
        private String _id;
        [JsonProperty("id")]
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private bool _isOwner = true;

        public bool IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value; }
        }
        private String _color;

        public String Color
        {
            get { return _color; }
            set { _color = value; }
        }
        private bool _inAll;

        public bool InAll
        {
            get { return _inAll; }
            set { _inAll = value; }
        }
        private long _sortOrder;

        public long SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private String _sortType;

        public String SortType
        {
            get { return _sortType; }
            set { _sortType = value; }
        }
        private int _userCount = 1;

        public int UserCount
        {
            get { return _userCount; }
            set { _userCount = value; }
        }
        private String _etag;

        public String Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        private DateTime _modifiedTime;

        public DateTime ModifiedTime
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }

        private bool? _closed;

        public bool? Closed
        {
            get { return _closed; }
            set { _closed = value; }
        }
    }
}
