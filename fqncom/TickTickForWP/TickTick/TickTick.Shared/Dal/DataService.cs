using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Dal
{
    public static class DataService
    {
        public static async void CreateDbAsync()
        {

            App.Connection = new SQLiteAsyncConnection("TickTick.db");

            var Attachment = App.Connection.CreateTableAsync<Attachment>();
            //var AttachmentSyncBean = App.Connection.CreateTableAsync<AttachmentSyncBean>();
            var ChecklistItem = App.Connection.CreateTableAsync<ChecklistItem>();
            var Comment = App.Connection.CreateTableAsync<Comment>();
            //var Items = App.Connection.CreateTableAsync<Items>();
            var Limits = App.Connection.CreateTableAsync<Limits>();
            //var Loc = App.Connection.CreateTableAsync<Loc>();
            var Location = App.Connection.CreateTableAsync<Location>();
            //var LocationSyncBean = App.Connection.CreateTableAsync<LocationSyncBean>();
            var ReferAttachment = App.Connection.CreateTableAsync<ReferAttachment>();
            var Projects = App.Connection.CreateTableAsync<Projects>();
            var SyncStatus = App.Connection.CreateTableAsync<SyncStatus>();
            var Tasks = App.Connection.CreateTableAsync<Tasks>();
            var TaskSyncedJson = App.Connection.CreateTableAsync<TaskSyncedJson>();
            //var TaskSyncedJsonBean = App.Connection.CreateTableAsync<TaskSyncedJsonBean>();
            var User = App.Connection.CreateTableAsync<User>();
            var UserProfile = App.Connection.CreateTableAsync<UserProfile>();
            var TaskReminder = App.Connection.CreateTableAsync<TaskReminder>();

            await Task.WhenAll(new Task[] 
            {
                User,
                UserProfile,
                Tasks,
                Projects,
                TaskReminder,
                Attachment,
                //AttachmentSyncBean,
                ChecklistItem,
                Comment,
                //Items,
                Limits,
                //Loc,
                Location,
                //LocationSyncBean,
                ReferAttachment,
                SyncStatus,
                TaskSyncedJson,
                //TaskSyncedJsonBean,
            });
        }
    }
}
