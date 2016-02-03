using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Dal
{
    public class ReferAttachmentDal : BaseDal<ReferAttachment>
    {
        public async Task CreateReferAttachment(ReferAttachment referAttachment)
        {

            if (await IsReferAttachmentExist(referAttachment))
            {
                return;
            }
            var conn = await CreateTableAsync();
            await conn.InsertAsync(referAttachment);
            //ContentValues values = makeContentValues(referAttachment);
            //long rowId = TABLE.create(values, dbHelper);
            //referAttachment.setId(rowId);
        }
        private async Task<bool> IsReferAttachmentExist(ReferAttachment referAttachment)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<ReferAttachment>().Where((r) => r.RefAttachmentSid == referAttachment.RefAttachmentSid && r.AttachmentSid == referAttachment.AttachmentSid).ToListAsync();
            return queryResult.Count > 0;
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(ReferAttachmentField.ref_attachment_sid.name()).append(" = ? and ")
            //            .append(ReferAttachmentField.user_Id.name()).append(" = ? and ")
            //            .append(ReferAttachmentField.attachment_sid.name()).append(" = ?");
            //    String[] selectionArgs = {
            //        referAttachment.getRefAttachmentSid(), referAttachment.getUserId(),
            //        referAttachment.getAttachmentSid()
            //};
            //return getReferAttachmentCount(selection.toString(), selectionArgs) > 0;
        }
    }
}
