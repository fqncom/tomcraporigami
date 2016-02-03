using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class AttachmentSyncBean
    {
        private List<Attachment> _added = new List<Attachment>();
        [Ignore]
        public List<Attachment> Added
        {
            get { return _added; }
            set { _added = value; }
        }
        private List<Attachment> _updated = new List<Attachment>();
        [Ignore]
        public List<Attachment> Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
        private List<Attachment> _deleted = new List<Attachment>();
        [Ignore]
        public List<Attachment> Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        public void AddAdded(Attachment add)
        {
            if (add != null)
            {
                this.Added.Add(add);
            }
        }
        public void AddAllAddeds(List<Attachment> addeds)
        {
            if (addeds != null)
            {
                this.Added.AddRange(addeds);
            }
        }

        public void AddDeleted(Attachment delete)
        {
            if (delete != null)
            {
                this.Deleted.Add(delete);
            }
        }
        public bool IsEmpty()
        {
            return Added.Count <= 0 && Updated.Count <= 0 && Deleted.Count <= 0;
        }
    }
}
