using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enums
{
    public class Constants
    {
        public static class Repeats
        {
            public static readonly int DOES_NOT_REPEAT = 0;
            public static readonly int REPEATS_DAILY = 1;
            public static readonly int REPEATS_EVERY_WEEKDAY = 2;
            public static readonly int REPEATS_WEEKLY_ON_DAY = 3;
            public static readonly int REPEATS_MONTHLY_ON_DAY = 4;
            public static readonly int REPEATS_MONTHLY_ON_DAY_COUNT = 5;
            public static readonly int REPEATS_YEARLY = 6;
            public static readonly int REPEATS_YEARLY_LUNAR = 7;
            public static readonly int REPEATS_CUSTOM = 8;
        }
        public static class SyncType
        {
            public static readonly int SYNC_TYPE_AUTO = 0;
            public static readonly int SYNC_TYPE_MANUL = 1;
        }
        public abstract class CompletedStatus
        {
            public static readonly int NORMAL = 0;
            public static readonly int DONE = 1;
            public static readonly int ARCHIVED = 2;

            public CompletedStatus() { }

            public static bool isCompleted(int status)
            {
                return (status == 1) || (status == 2);
            }
        }
        public static readonly bool RELEASE = false;
        public static class OrderStepData
        {
            public static readonly long STEP = 1L << 38;
        }
        public static class TaskStatus
        {
            public static readonly int UNCOMPLETED = 0;
            public static readonly int COMPLETED = 1;
            public static readonly int ARCHIVED = 2;
        }
        public static class RepeatFromStatus
        {
            public static readonly String DUEDATE = "0";
            public static readonly String COMPLETETIME = "1";
            //默认从duedate开始循环
            public static readonly String DEFAULT = "2";
        }
        public static class SyncFileErrorCode
        {
            public static readonly int NO_ERROR = 0;
            public static readonly int DOWNLOAD_NO_FILE = 2;
            public static readonly int NETWORK_ERROR = 4;
        }

        public static class Kind
        {
            public const string TEXT = "TEXT";

            public const string CHECKLIST = "CHECKLIST";

            public static string GetKind(String type)
            {
                if (!string.IsNullOrEmpty(type) && string.Equals(CHECKLIST, type))
                {
                    return CHECKLIST;
                }
                return TEXT;

            }

            public static string GetKind(int value)
            {
                switch (value)
                {
                    case 1:
                        return TEXT;
                    case 2:
                        return CHECKLIST;
                    default:
                        return TEXT;
                }
            }
        }

        public class Removed
        {
            public static readonly long ASSIGNEE = long.MinValue;

            public Removed() { }
        }

        public static class AccountType
        {
            public readonly static int ACCOUNT_TYPE_TICKTICK_NORMAL = 2;
            public readonly static int ACCOUNT_TYPE_TICKTICK_GOOGLE_SYSTEM = 3;
            public readonly static int ACCOUNT_TYPE_LOCAL_MODE = 4;
            public readonly static int ACCOUNT_TYPE_FACEBOOK = 5;
            public readonly static int ACCOUNT_TYPE_TICKTICK_GOOGLE_WEB = 6;
            public readonly static int ACCOUNT_TYPE_TENCENT = 7;
            public readonly static int ACCOUNT_TYPE_SINA_WEIBO = 8;
            public readonly static int ACCOUNT_TYPE_WEIXIN = 9;
            public readonly static int ACCOUNT_TYPE_TICKTICK_TWITTER_WEB = 10;
            public readonly static int ACCOUNT_TYPE_FREE = 0;
            public readonly static int ACCOUNT_TYPE_PREMIUM = 1;
        }
        public static class EntityIdentifie
        {
            public static readonly long CALENDAR_ID = 10029732L;
            public static readonly long ALL_ID = -1L;
            public static readonly int DEFAULT_TASK_ID = -1;
            public static readonly int INVALID_PROJECT_ID = -1;
            public static readonly int NEW_PROJECT_DEFAULT_ID = 0;

            public static readonly String CALENDAR_TAB = "_calendar_tab_";

            public static readonly String LOCAL_INBOX_ID = "local_inbox_id";

            public static readonly long DEFAULT_USER_PROFILE_ID = -1000000L;

            public static readonly long DEFAULT_LOCATION_ID = -1;

            public static readonly long DEFAULT_NEW_ID = -1L;
        }
        public static class MeridiemType
        {
            public static readonly int TYPE_24_HOUR = 0;
            public static readonly int TYPE_AM_PM = 1;
        }
    }


}
