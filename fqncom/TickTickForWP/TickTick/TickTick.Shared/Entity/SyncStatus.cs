using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class SyncStatus
    {
        public SyncStatus()
        {

        }
        public SyncStatus(String userId, String entityId, int type)
        {
            this.UserId = userId;
            this.EntityId = entityId;
            this.Type = type;
        }
        public SyncStatus(String userId, String entityId, int type, String moveFromId)
        {
            this.UserId = userId;
            this.EntityId = entityId;
            this.Type = type;
            this.MoveFromId = moveFromId;
        }

        private int _id;
        [PrimaryKey]
        [AutoIncrement]
        [JsonIgnore]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _entityId;

        public string EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }
        private int _type;

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _moveFromId;

        public string MoveFromId
        {
            get { return _moveFromId; }
            set { _moveFromId = value; }
        }

    }
}
