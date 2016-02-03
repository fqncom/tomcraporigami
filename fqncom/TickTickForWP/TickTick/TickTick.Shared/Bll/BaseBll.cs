using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Bll
{
    public abstract class BaseBll<T> : IBaseBll<T> where T : class,new()
    {
        public BaseBll()
        {
            SetCurrentDal();
        }

        protected BaseDal<T> CurrentDal { get; set; }

        protected abstract void SetCurrentDal();

        #region IBaseBll<T> 成员

        public async Task<List<T>> ExecuteTable()
        {
            return await CurrentDal.ExecuteTable();
        }

        public Task<int> DeleteData(T t)
        {
            return CurrentDal.DeleteData(t);
        }

        public Task<int> InsertAsync(T t)
        {
            return CurrentDal.InsertAsync(t);
        }

        public Task<int> InsertAllAsync(List<T> t)
        {
            return CurrentDal.InsertAllAsync(t);
        }

        public Task<int> UpdateAsync(T t)
        {
            return CurrentDal.UpdateAsync(t);
        }

        public Task<int> UpdateAllAsync(List<T> t)
        {
            return CurrentDal.UpdateAllAsync(t);
        }

        public Task<int> DropTable()
        {
            return CurrentDal.DropTable();
        }

        #endregion

        #region 非公共成员
        public async Task<int> DeleteForever(T t)
        {
            var entity = t as BaseEntity;
            //entity.Deleted = ModelStatusEnum.DELETED_FOREVER;
            entity.Type = ModelStatusEnum.SYNC_TYPE_TASK_TRASH;
            return await this.UpdateAsync(t);
        }

        //public async Task<List<T>> GetAllTasksByDelFlag(int delStatus)
        //{
        //    return await CurrentDal.GetAllTasksByDelFlag(delStatus);
        //}

        #endregion
    }
}
