using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using Windows.Storage;

namespace TickTick.Dal
{
    public class SyncStatusDal : BaseDal<SyncStatus>
    {
        #region IBaseDal<SyncStatus> 成员

        //public async Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    var conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
        //    await conn.CreateTableAsync<SyncStatus>();
        //    return conn;
        //}

        //public Task<List<SyncStatus>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<SyncStatus>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(SyncStatus t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(SyncStatus t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<SyncStatus> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(SyncStatus t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<SyncStatus> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task<HashSet<String>> GetEntityIdsByType(String userId, int type)
        {
            HashSet<String> ids = new HashSet<String>();
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.Type == type).ToListAsync();
            foreach (var item in queryResult)
            {
                ids.Add(item.EntityId);
            }
            return ids;
        }
        public async Task<Dictionary<String, String>> GetMoveFromIdDic(String userId, int type)
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.Type == type && s.UserId == userId).ToListAsync();
            foreach (var item in queryResult)
            {
                if (dic.ContainsKey(item.EntityId))
                {
                    dic[item.EntityId] = item.MoveFromId;
                }
                else
                {
                    dic.Add(item.EntityId, item.MoveFromId);
                }
            }
            return dic;
        }
        public async Task DeleteSyncStatusForever(String userId, String entityId, int type)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.EntityId == entityId && s.Type == type).FirstOrDefaultAsync();
            if (queryResult != null)
            {
                await conn.DeleteAsync(queryResult);
            }
        }
        public async Task<bool> UpdateMoveFromId(String taskSid, String userId, String moveFromId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.EntityId == taskSid && s.UserId == userId).FirstOrDefaultAsync();
            queryResult.MoveFromId = moveFromId;
            return (await conn.UpdateAsync(queryResult)) > 0;
        }
        public async Task DeleteSyncStatusPhysical(String userId, String entityId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.EntityId == entityId).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }
        }

        public async Task<SyncStatus> AddSyncStatus(SyncStatus status)
        {
            var conn = await CreateTableAsync();
            await conn.InsertAsync(status);
            return status;

            //ContentValues values = makeContentValues(status);
            //long id = table.create(values, dbHelper);
            //status.setId(id);
            //return status;
        }
        public async Task<Dictionary<int, SyncStatus>> GetSyncStatusDic(String userId, String entityId)
        {
            Dictionary<int, SyncStatus> syncStatusDic = new Dictionary<int, SyncStatus>();

            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.EntityId == entityId).ToListAsync();
            foreach (var item in queryResult)
            {
                if (syncStatusDic.ContainsKey(item.Type))
                {
                    syncStatusDic[item.Type] = item;
                }
                else
                {
                    syncStatusDic.Add(item.Type, item);
                }
            }
            return syncStatusDic;
        }
        public async Task DeleteSyncStatusForeverExceptType(String userId, String entityId, int exceptType)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.EntityId == entityId && s.Type != exceptType).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(queryResult);
            }
        }
        #endregion

        #region android代码

        //public Map<Integer, SyncStatus> getSyncStatusMap(String userId, String entityId)
        //{
        //    Map<Integer, SyncStatus> syncStatusMap = new HashMap<Integer, SyncStatus>();
        //    Cursor c = null;
        //    StringBuilder selection = new StringBuilder();
        //    selection.append(SyncStatusField.user_id.name()).append(" = ? and ")
        //            .append(SyncStatusField.entity_id.name()).append(" = ?");
        //    String[] selectionArgs = {
        //        userId, entityId
        //};
        //    try
        //    {
        //        c = table.query(selection.toString(), selectionArgs, null, dbHelper);
        //        if (c != null && c.moveToFirst())
        //        {
        //            do
        //            {
        //                SyncStatus syncStatus = cursorToStatus(c);
        //                syncStatusMap.put(syncStatus.getType(), syncStatus);
        //            } while (c.moveToNext());
        //        }
        //        return syncStatusMap;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //}
        //public boolean updateMoveFromId(String taskSid, String userId, String moveFromId)
        //{
        //    ContentValues values = new ContentValues();
        //    values.put(SyncStatusField.move_from_id.name(), moveFromId);
        //    StringBuffer whereClause = new StringBuffer();
        //    whereClause.append(SyncStatusField.entity_id.name()).append(" = ? and ")
        //            .append(SyncStatusField.user_id.name()).append(" = ?");
        //    String[] whereArgs = new String[] {
        //        taskSid, userId
        //};
        //    int count = table.update(values, whereClause.toString(), whereArgs, dbHelper);
        //    return count > 0;
        //}
        //public void deleteSyncStatusForever(String userId, String entityId, int type)
        //{
        //    StringBuffer sql = new StringBuffer();
        //    sql.append("DELETE FROM ").append(table.tableName()).append(" WHERE ")
        //            .append(SyncStatusField.user_id.name()).append(" = '").append(userId)
        //            .append("' and ").append(SyncStatusField.entity_id.name()).append(" = '")
        //            .append(entityId).append("' and ").append(SyncStatusField._type.name())
        //            .append(" = ").append(type);
        //    dbHelper.getWritableDatabase().execSQL(sql.toString());
        //}
        //public Map<String, String> getMoveFromIdMap(String userId, int type)
        //{
        //    Map<String, String> map = new HashMap<String, String>();
        //    Cursor c = null;
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(SyncStatusField._type.name()).append(" = ? and ")
        //            .append(SyncStatusField.user_id.name()).append(" = ?");
        //    String[] selectionArgs = new String[] {
        //        type + "", userId
        //};

        //    try
        //    {
        //        c = table.query(selection.toString(), selectionArgs, null, dbHelper);
        //        c.moveToFirst();
        //        while (!c.isAfterLast())
        //        {
        //            String id = c.getString(SyncStatusField.entity_id.ordinal());
        //            String moveFromId = c.getString(SyncStatusField.move_from_id.ordinal());
        //            map.put(id, moveFromId);
        //            c.moveToNext();
        //        }
        //        return map;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }

        //}
        //public Set<String> getEntityIdsByType(String userId, int type)
        //{
        //    Set<String> ids = new HashSet<String>();

        //    Cursor c = null;
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(SyncStatusField._type.name()).append(" = ? and ")
        //            .append(SyncStatusField.user_id.name()).append(" = ? ");
        //    String[] selectionArgs = new String[] {
        //         type + "", userId
        // };

        //    try
        //    {
        //        c = table.query(selection.toString(), selectionArgs, null, dbHelper);
        //        c.moveToFirst();
        //        while (!c.isAfterLast())
        //        {
        //            ids.add(c.getString(SyncStatusField.entity_id.ordinal()));
        //            c.moveToNext();
        //        }
        //        return ids;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //}
        #endregion


    }
}
