using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Models;

namespace TickTick.Bll
{
    public class TaskSyncedJsonBll : BaseBll<TaskSyncedJson>
    {
        private TaskSyncedJsonDal TaskSyncedJsonDal = new TaskSyncedJsonDal();

        #region 自定义代码

        public async Task<Dictionary<String, TaskSyncedJson>> GetAllTaskSyncedJsonDic(String userID)
        {
            Dictionary<String, TaskSyncedJson> idToItems = new Dictionary<String, TaskSyncedJson>();
            List<TaskSyncedJson> jsons = await TaskSyncedJsonDal.GetAllTaskSyncedJsons(userID);
            foreach (var taskSyncedJson in jsons)
            {
                if (idToItems.ContainsKey(taskSyncedJson.TaskSID))
                {
                    idToItems[taskSyncedJson.TaskSID] = taskSyncedJson;
                }
                else
                {
                    idToItems.Add(taskSyncedJson.TaskSID, taskSyncedJson);
                }
            }
            return idToItems;
        }
        public async Task<bool> SaveTaskSyncedJsons(TaskSyncedJsonBean bean, String userID)
        {
            bool result = true;
            //ObjectMapper mapper = new ObjectMapper();          使用newton.json代替进行对象转换操作
            //已经完成删除的任务，对应的Original版本需要被清除
            foreach (var json in bean.Deleted)
            {
                await TaskSyncedJsonDal.DeleteTaskSyncedJsonForever(json.TaskSID, json.UserId);
            }
            //try
            //{
            foreach (var addTask in bean.Added)
            {
                result |= await CreateTaskSyncedJsonByTask(addTask, userID);
            }

            foreach (var updateTask in bean.Updated)
            {
                bool ret = await UpdateTaskSyncedJsonByTask(updateTask, userID);
                if (ret)
                {
                    result |= ret;
                }
                else
                {
                    // 更新失败时，改为创建
                    result |= await CreateTaskSyncedJsonByTask(updateTask, userID);
                }
            }
            return result;
            //}
            //catch (JsonProcessingException e) {
            //    Log.e(TAG, e.getMessage(), e);
            //    result = false;
            //}
        }
        public async Task<bool> UpdateTaskSyncedJsonByTask(TasksServer task, String userID)
        {
            TaskSyncedJson json = new TaskSyncedJson();
            json.TaskSID = task.Id;
            json.UserId = userID;
            json.JsonString = JsonConvert.SerializeObject(task);//mapper.writeValueAsString(task);
            return await TaskSyncedJsonDal.UpdateTaskSyncedJson(json);
        }
        public async Task<bool> CreateTaskSyncedJsonByTask(TasksServer task, String userID)
        {
            String jsonString = JsonConvert.SerializeObject(task);// 使用newton.json进行对象的序列化，而不是使用objectMapper
            TaskSyncedJson json = new TaskSyncedJson();
            json.TaskSID = task.Id;
            json.UserId = userID;
            json.JsonString = jsonString;
            json = await TaskSyncedJsonDal.CreateTaskSyncedJson(json);
            return json.Id != 0;
        }
        #endregion

        #region android代码
        //    public boolean saveTaskSyncedJsons(final TaskSyncedJsonBean bean, final String userID) {
        //    return dbHelper.doInTransaction(new Transactable<Boolean>() {

        //        @Override
        //        public Boolean doIntransaction(GTasksDBHelper dbHelper) {
        //            boolean result = true;
        //            ObjectMapper mapper = new ObjectMapper();
        //            //已经完成删除的任务，对应的Original版本需要被清除
        //            for (TaskSyncedJson json : bean.getDeleted()) {
        //                taskSyncedJsonDao.deleteTaskSyncedJsonForever(json.getTaskSID(),
        //                        json.getUserID());
        //            }
        //            try {
        //                for (Task addTask : bean.getAdded()) {
        //                    result |= createTaskSyncedJsonByTask(mapper, addTask, userID);
        //                }

        //                for (Task updateTask : bean.getUpdated()) {
        //                    boolean ret = updateTaskSyncedJsonByTask(mapper, updateTask, userID);
        //                    if (ret) {
        //                        result |= ret;
        //                    } else {
        //                        // 更新失败时，改为创建
        //                        result |= createTaskSyncedJsonByTask(mapper, updateTask, userID);
        //                    }
        //                }
        //            } catch (JsonProcessingException e) {
        //                Log.e(TAG, e.getMessage(), e);
        //                result = false;
        //            }

        //            return result;
        //        }
        //    });
        //} 
        #endregion

        protected override void SetCurrentDal()
        {
            CurrentDal = TaskSyncedJsonDal;
        }

        public async Task DeleteTaskSyncedJsonPhysical(String taskSid, String userId)
        {
            await TaskSyncedJsonDal.DeleteTaskSyncedJsonPhysical(taskSid, userId);
        }
    }
}
