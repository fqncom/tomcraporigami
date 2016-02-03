using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Entity
{
    public class ReferAttachment
    {

        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _attachmentSid;

        public String AttachmentSid
        {
            get { return _attachmentSid; }
            set { _attachmentSid = value; }
        }
        private String _refAttachmentSid;

        public String RefAttachmentSid
        {
            get { return _refAttachmentSid; }
            set { _refAttachmentSid = value; }
        }
        private String _userId;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
    }
}
