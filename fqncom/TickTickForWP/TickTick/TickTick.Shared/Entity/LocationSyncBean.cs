using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class LocationSyncBean
    {
        private List<Location> _updateLocations = new List<Location>();
        [Ignore]
        public List<Location> UpdateLocations
        {
            get { return _updateLocations; }
            set { _updateLocations = value; }
        }


        private List<Location> _deleteLocations = new List<Location>();
        [Ignore]
        public List<Location> DeleteLocations
        {
            get { return _deleteLocations; }
            set { _deleteLocations = value; }
        }
        public void AddUpdateLocation(Location update)
        {
            if (update != null)
            {
                this.UpdateLocations.Add(update);
            }
        }

        public void AddDeleteLocation(Location delete)
        {
            if (delete != null)
            {
                this.DeleteLocations.Add(delete);
            }
        }
        public bool IsEmpty()
        {
            return UpdateLocations.Count <= 0 && DeleteLocations.Count <= 0;
        }
    }
}
