using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Dal
{
    public class LocationDal : BaseDal<Location>
    {
        #region IBaseDal<Location> 成员

        //public Task<SQLite.SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Location>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Location>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(Location t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(Location t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<Location> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(Location t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<Location> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task UpdateLocationStatus(int status, long locationId, String userId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.UserId == userId && l.Id == locationId).FirstOrDefaultAsync();
            queryResult.AlertStatus = status;

            queryResult.ModifiedTime = DateTime.UtcNow;
            await conn.UpdateAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(LocationField.alert_status.name(), status);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(LocationField.user_Id.name()).append(" = ? and ")
            //            .append(LocationField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        userId, locationId + ""
            //};
            //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task<Location> GetLocationsByTaskId(long taskId, bool withDeleted)
        {
            var queryResultTask = (await ExecuteAsyncQueryTable()).Where((l) => l.TaskId == taskId);
            if (!withDeleted)
            {
                queryResultTask = queryResultTask.Where((l) => l.Deleted == ModelStatusEnum.DELETED_NO);
            }
            return await queryResultTask.FirstOrDefaultAsync();
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(LocationField.task_id.name()).append(" = ?");
            //    if (!withDeleted)
            //    {
            //        selection.append(" and ").append(LocationField._deleted.name()).append(" = ")
            //                .append(Status.DELETED_NO);
            //    }
            //    String[] selectionArgs = new String[] {
            //        taskId + ""
            //};
            //    ArrayList<Location> locations = getAllLocation(selection.toString(), selectionArgs, null);
            //    return locations.isEmpty() ? null : locations.get(0);
        }
        public async Task DeleteLocationLogicById(long locationId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.Id == locationId).FirstOrDefaultAsync();

            queryResult.Deleted = ModelStatusEnum.DELETED_TRASH;
            queryResult.Status = ModelStatusEnum.SYNC_UPDATE;
            queryResult.ModifiedTime = DateTime.UtcNow;
            await conn.UpdateAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(LocationField._deleted.name(), Status.DELETED_TRASH);
            //    values.put(LocationField._status.name(), Status.SYNC_UPDATE);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(LocationField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        locationId + ""
            //};
            //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task UpdateLocationSyncStatus(int status, long locationId, String userId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.UserId == userId && l.Id == locationId).FirstOrDefaultAsync();
            queryResult.Status = status;
            queryResult.ModifiedTime = DateTime.UtcNow;
            await conn.UpdateAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(LocationField._status.name(), status);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(LocationField.user_Id.name()).append(" = ? and ")
            //            .append(LocationField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        userId, locaitonId + ""
            //};
            //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task ExchangeToNewTaskSid(String userId, String taskSid, String newTaskSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.UserId == userId && l.TaskSid == taskSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.TaskSid = newTaskSid;
                item.Status = ModelStatusEnum.SYNC_NEW;
            }
            await conn.UpdateAllAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(LocationField.task_sid.name(), newTaskSid);
            //    values.put(LocationField._status.name(), Status.SYNC_NEW);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(LocationField.user_Id.name()).append(" = ? and ").append(LocationField.task_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, taskSid
            //};
            //    TABLE.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task<List<Location>> GetLocationsByUserId(string userId, bool withDeleted)
        {
            //var conn = await CreateTableAsync();
            //var queryResultTask = conn.Table<Location>().Where((l) => l.UserId == userId);
            var queryResultTask = (await ExecuteAsyncQueryTable()).Where((l) => l.UserId == userId);
            if (!withDeleted)
            {
                queryResultTask = queryResultTask.Where((l) => l.Deleted == ModelStatusEnum.DELETED_NO);
            }
            return await queryResultTask.ToListAsync();
        }
        public async Task DeleteLocationsPhysicalByTaskId(int taskId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.TaskId == taskId).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }
        }
        public async Task InsertLocation(Location location)
        {
            var conn = await CreateTableAsync();
            await conn.InsertAsync(location);

            //ContentValues values = getContentValue(location);
            //long id = TABLE.create(values, dbHelper);
            //location.setId(id);
        }
        public async Task UpdateLocation(Location location)
        {
            var conn = await CreateTableAsync();
            location.ModifiedTime = DateTime.UtcNow;
            await conn.UpdateAsync(location);

            //    ContentValues values = getContentValue(location);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(LocationField._id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        location.getId() + ""
            //};
            //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task DeleteLocatonForever(long id)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Location>().Where((l) => l.Id == id).FirstOrDefaultAsync();
            await conn.DeleteAsync(queryResult);

            //TABLE.deleteById(LocationField._id, id, dbHelper);
        }
        #endregion

        #region android代码

        //private ArrayList<Location> getAllLocation(String selection, String[] selectionArgs,
        //    Field orderBy)
        //{
        //    Cursor c = null;
        //    ArrayList<Location> locations = new ArrayList<Location>();
        //    try
        //    {
        //        c = TABLE.query(selection, selectionArgs, orderBy, dbHelper);
        //        c.moveToFirst();
        //        while (!c.isAfterLast())
        //        {
        //            locations.add(convertCursorToLocation(c));
        //            c.moveToNext();
        //        }
        //        return locations;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //}
        //public ArrayList<Location> getLocationsByUserId(String userId, boolean withDeleted)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(LocationField.user_Id.name()).append(" = ?");
        //    if (!withDeleted)
        //    {
        //        selection.append(" and ").append(LocationField._deleted.name()).append(" = ")
        //                .append(Status.DELETED_NO);
        //    }

        //    String[] selectionArgs = new String[] {
        //        userId
        //};
        //    return getAllLocation(selection.toString(), selectionArgs, null);
        //}
        #endregion

        public async Task<Dictionary<int, Location>> GetLocationsByTaskIds(HashSet<long> taskIds, bool withDeleted)
        {
            Dictionary<int, Location> map = new Dictionary<int, Location>();
            StringBuilder selection = new StringBuilder();
            var queryResult = await ExecuteAsyncQueryTable();
            if (!withDeleted)
            {
                queryResult = queryResult.Where(l => l.Deleted == ModelStatusEnum.DELETED_NO);
            }
            List<Location> allLocation = await GetDataTableByExpression((l) => taskIds.Contains(l.TaskId), null);// TODO 使用contain，效率问题？
            foreach (var item in allLocation)
            {
                if (map.ContainsKey(item.TaskId))
                {
                    map[item.TaskId] = item;
                }
                else
                {
                    map.Add(item.TaskId, item);
                }
            }
            return map;
            //if (!withDeleted) {
            //    selection.ppend(LocationField._deleted.name()).append(" = ").append(Status.DELETED_NO);
            //}
            //DBUtils.appendInLongIds(selection, LocationField.task_id.nameWithTable(), taskIds);
            //ArrayList<Location> locations = getAllLocation(selection.toString(), null, null);
            //for (Location location : locations) {
            //    if (!map.containsKey(location.getTaskId())) {
            //        map.put(location.getTaskId(), location);
            //    }
            //}
            //return map;
        }
        
    }
}
