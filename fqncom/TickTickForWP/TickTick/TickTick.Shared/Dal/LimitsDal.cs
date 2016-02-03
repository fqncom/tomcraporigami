using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Dal
{
    public class LimitsDal : BaseDal<Limits>
    {

        #region IBaseDal<Limits> 成员

        //public Task<SQLite.SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Limits>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Limits>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(Limits t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(Limits t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<Limits> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(Limits t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<Limits> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task<Limits> GetLimits(int accountType)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<Limits>().Where((l) => l.AccountType == accountType).FirstOrDefaultAsync();
        }
        #endregion

        #region android代码

        //public Limits GetLimits(int accountType)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(LimitsField.account_type.name()).append(" =?");
        //    String[] selectionArgs = new String[] {
        //    accountType + ""
        //};
        //    List<Limits> limits = getAllLimits(selection.toString(), selectionArgs, null);
        //    return limits.isEmpty() ? null : limits.get(0);
        //}
        #endregion
    }
}
