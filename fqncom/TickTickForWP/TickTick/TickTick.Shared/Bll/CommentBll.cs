using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;

namespace TickTick.Bll
{
    public class CommentBll : BaseBll<Comment>
    {
        private CommentDal CommentDal = new CommentDal();

        public async Task DeleteCommentsByTaskSIdForever(String taskSId, String userId)
        {
            await CommentDal.DeleteCommentsByTaskSIdForever(taskSId, userId);
        }

        protected override void SetCurrentDal()
        {
            CurrentDal = CommentDal;
        }
    }
}
