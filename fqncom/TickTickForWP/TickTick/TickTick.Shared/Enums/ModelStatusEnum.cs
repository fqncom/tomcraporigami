using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enums
{
    public static class ModelStatusEnum
    {
        public readonly static int Task_Not_Finished = 0;
        public readonly static int Task_Finished = 1;// TODO 此处有坑，未确定是1 还是2

        public readonly static int SYNC_NEW = 0;
        public readonly static int SYNC_UPDATE = 1;
        public readonly static int SYNC_DONE = 2;

        public readonly static int PROJECT_NEED_PULL_TASKS = 1;
        public readonly static int PROJECT_DONT_NEED_PULL_TASKS = 0;

        public readonly static int COMPLETED = 1;
        public readonly static int NOT_COMPLETED = 0;

        public readonly static int PRIORITY_NORMAL = 0;
        public readonly static int PRIORITY_IMPORTANT = 1;

        public readonly static int NORMAL_PROJECT = 0;
        public readonly static int DEFAULT_PROJECT = 1;

        public readonly static int DELETED_FOREVER = 2;
        public readonly static int DELETED_TRASH = 1;
        public readonly static int DELETED_NO = 0;

        public readonly static int ACCOUNT_FREEZE = 0;
        public readonly static int ACCOUNT_ACTIVE = 1;

        public readonly static int SLEEP = 0;
        public readonly static int WAKE = 1;

        /**
         * sync status
         */
        public readonly static int SYNC_TYPE_TASK_CONTENT = 0;
        public readonly static int SYNC_TYPE_TASK_ORDER = 1;
        public readonly static int SYNC_TYPE_TASK_MOVE = 2;
        public readonly static int SYNC_TYPE_TASK_ASSIGN = 3;
        public readonly static int SYNC_TYPE_TASK_CREATE = 4;
        public readonly static int SYNC_TYPE_TASK_TRASH = 5;
        public readonly static int SYNC_TYPE_TASK_DELETE_FOREVER = 6;
        public readonly static int SYNC_TYPE_TASK_RESTORE = 7;

        public readonly static int ALERT_STATUS_NORMAL = 0;
        public readonly static int ALERT_STATUS_FIRED = 1;
        public readonly static int ALERT_STATUS_DONE = 2;

        public readonly static int ALERT_STATUS_ENTER_REDIRECT = 3;
        public readonly static int ALERT_STATUS_EXIT_REDIRECT = 4;

        public readonly static int ACCOUNT_DISABLED_NO = 0;
        public readonly static int ACCOUNT_DISABLED_YES = 1;

        public readonly static int YES = 1;
        public readonly static int NO = 0;

        public static readonly int UP_DOWN_DONE = 0;
        public static readonly int UP_DOWN_UNDOWNLOAD = 1;
        public static readonly int UP_DOWN_NEED_TO_DOWNLOAD = 2;
        public static readonly int UP_DOWN_UNUPLOAD = 3;
        public static readonly int UP_DOWN_NEED_TO_UPLOAD = 4;

        public static readonly int CLOSED_NO = 0;
        public static readonly int CLOSED_YES = 1;
    }

}
