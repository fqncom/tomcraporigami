using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Models
{
    public class TaskSyncBean
    {
        private List<Tasks> _added = new List<Tasks>();

        public List<Tasks> Added
        {
            get { return _added; }
            set { _added = value; }
        }
        private List<Tasks> _updated = new List<Tasks>();

        public List<Tasks> Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
        private List<Tasks> _updating = new List<Tasks>();

        public List<Tasks> Updating
        {
            get { return _updating; }
            set { _updating = value; }
        }
        private List<Tasks> _deletedInTrash = new List<Tasks>();

        public List<Tasks> DeletedInTrash
        {
            get { return _deletedInTrash; }
            set { _deletedInTrash = value; }
        }
        private List<Tasks> _deletedForever = new List<Tasks>();

        public List<Tasks> DeletedForever
        {
            get { return _deletedForever; }
            set { _deletedForever = value; }
        }

        public void AddToDeletedForever(Tasks delete)
        {
            if (delete != null)
            {
                DeletedForever.Add(delete);
            }
        }
        public void AddToDeletedInTrash(Tasks delete)
        {
            if (delete != null)
            {
                DeletedInTrash.Add(delete);
            }
        }
        public void AddToUpdating(Tasks updating)
        {
            if (updating != null)
            {
                Updated.Add(updating);
            }
        }
        public void AddToUpdated(Tasks update)
        {
            if (update != null)
            {
                Updated.Add(update);
            }
        }
        public void AddToAdded(Tasks add)
        {
            if (add != null)
            {
                Added.Add(add);
            }
        }
        public bool IsEmpty()
        {
            return this.Added.Count <= 0 && this.Updated.Count <= 0 && this.Updating.Count <= 0 && this.DeletedInTrash.Count <= 0 && DeletedForever.Count <= 0;
        }
    }
}
