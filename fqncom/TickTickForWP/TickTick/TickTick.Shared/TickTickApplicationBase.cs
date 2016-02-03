using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using TickTick.Synchronous;
using TickTick.Entity;
using TickTick.Manager;
using TickTick.Bll;

namespace TickTick
{
    public class TickTickApplicationBase : Application, GoogleTaskManager.TasksListener
    {
        //private WebTaskManager webTaskManager;
        private ProjectBll projectBll;
        private TaskBll taskBll;
        private AttachmentBll attachmentBll;
        private ChecklistItemBll checklistItemBll;
        private SyncStatusBll syncStatusBll;
        //private UserProfileBll userProfileBll;
        //private CalendarProjectBll calendarProjectBll;
        //private CalendarSubscribeProfileBll calendarSubscribeProfileBll;
        //private CalendarEventBll calendarEventBll;
        //private CalendarReminderBll calendarReminderBll;
        private CommentBll commentBll;
        private bool needRestartActivity = false;
        private bool gpRestarted = false;

        private bool isWifiEnable = true;

        private bool isNetworkEnable = true;

        private TickTickAccountManager accountManager;
        private bool isNeedRelogin = false;
        //private TickTickDBHelper dbHelper;
        private bool isSendBroadcast = false;
        private bool needResyncUserStatus = false;
        private bool needSync = false;
        private static TickTickApplicationBase staticApplication;
        //private AnalyticsInstance analyticsInstance;

        //protected Handler handler = new Handler();

        public volatile int activeActivities = 0;
        /// <summary>
        /// 单例化当前对象
        /// </summary>
        private static TickTickApplicationBase _staticApplication;

        public static TickTickApplicationBase StaticApplication
        {
            get
            {
                if (_staticApplication == null)
                {
                    lock ("create")
                    {
                        if (_staticApplication == null)
                        {
                            _staticApplication = new TickTickApplicationBase();
                        }
                    }
                }
                return _staticApplication;
            }
        }
        public String GetDailyReminderTimeFlag()
        {
            UserProfile profile = App.SignUserInfo.UserProfile;//GetAccountManager().getCurrentUser().getUserProfile();
            return profile == null ? "-1" : profile.DailyReminderTime;
        }
        public TickTickAccountManager GetAccountManager()
        {
            return this.accountManager;
        }


        #region android实现代码
        //private static final String TAG = TickTickApplicationBase.class.getSimpleName();
        //public static boolean screenOff = true;
        //private static IntentFilter settingsIntentFilter;

        //static {
        //    settingsIntentFilter = new IntentFilter();
        //    settingsIntentFilter.addAction(Intent.ACTION_TIME_CHANGED);
        //    settingsIntentFilter.addAction("android.location.PROVIDERS_CHANGED");
        //}

        //private WebTaskManager webTaskManager;
        //private ProjectService projectService;
        //private TaskService taskService;
        //private AttachmentService attachmentService;
        //private ChecklistItemService checklistItemService;
        //private SyncStatusService syncStatusService;
        //private UserProfileService userProfileService;
        //private CalendarProjectService calendarProjectService;
        //private CalendarSubscribeProfileService calendarSubscribeProfileService;
        //private CalendarEventService calendarEventService;
        //private CalendarReminderService calendarReminderService;
        //private CommentService commentService;
        //private SettingsPreferencesHelper preferencesHelper;
        //private boolean needRestartActivity = false;
        //private boolean gpRestarted = false;

        //private boolean isWifiEnable = true;

        //private boolean isNetworkEnable = true;

        //private TickTickAccountManager accountManager;
        //private boolean isNeedRelogin = false;
        //private TickTickDBHelper dbHelper;
        //private boolean isSendBroadcast = false;
        //private boolean needResyncUserStatus = false;
        //private boolean needSync = false;
        //private static TickTickApplicationBase staticApplication;
        //private AnalyticsInstance analyticsInstance;

        //private Handler handler = new Handler();

        //public volatile int activeActivities = 0;

        //public boolean isWifiEnable() {
        //    return isWifiEnable;
        //}

        //public boolean isNetworkEnable() {
        //    return isNetworkEnable;
        //}

        //public boolean isPreferencesRestarted() {
        //    return gpRestarted;
        //}

        //public void setPreferencesRestarted(boolean restarted) {
        //    this.gpRestarted = restarted;
        //}

        //public boolean isNeedRestartActivity() {
        //    return needRestartActivity;
        //}

        //public void setNeedRestartActivity(boolean needRestartActivity) {
        //    this.needRestartActivity = needRestartActivity;
        //}

        //public TickTickAccountManager getAccountManager() {
        //    return this.accountManager;
        //}

        //public BackgroundTaskManager getBackgroundTaskManagerMe() {
        //    return webTaskManager.getBackgroundTaskManager();
        //}

        //public WebTaskManager getWebTaskManager() {
        //    return webTaskManager;
        //}

        //public TaskService getTaskService() {
        //    return taskService;
        //}

        //public AttachmentService getAttachmentService() {
        //    return attachmentService;
        //}

        //public ProjectService getProjectService() {
        //    return projectService;
        //}

        //public SyncStatusService getSyncStatusService() {
        //    return syncStatusService;
        //}

        //public boolean getNeedRelogin() {
        //    return isNeedRelogin;
        //}

        //public void setNeedRelogin(boolean isNeedRelogin) {
        //    this.isNeedRelogin = isNeedRelogin;
        //}

        //public boolean needSync() {
        //    return needSync;
        //}

        //public void setNeedSync(boolean needSync) {
        //    this.needSync = needSync;
        //}

        //public static TickTickApplicationBase getInstance() {
        //    return staticApplication;
        //}

        //@Override
        //public void onCreate() {
        //    super.onCreate();
        //    staticApplication = this;
        //    UriBuilder.create(this);
        //    IntentParamsBuilder.create(this);
        //    analyticsInstance = initAnalyticsInstance();
        //    dbHelper = initDBHelper();
        //    dbHelper.getWritableDatabase();
        //    initDBServices();
        //    accountManager = initAccountManager();
        //    Crashlytics.setUserEmail(accountManager.getCurrentUser().getUsername());
        //    isNeedRelogin = true;
        //    if (!getPreferencesHelper().isStatusBarShow()) {
        //        NotificationOngoing.cancelNotification(this);
        //    }
        //    registerNetworkState();
        //    showMissReminderDialog();
        //    tryToSendTaskAlertScheduleBroadcast();
        //    sendNotificationOngoingBroadcast(-1);
        //    String calendarUri = CalendarUtil.getCalendarUriBase(this);
        //    if (calendarUri != null) {
        //        getContentResolver().registerContentObserver(Uri.parse(calendarUri), true,
        //                calendarChangeObserver);
        //    }
        //    CommunicationHelper.getInstance().init(this);
        //    webTaskManager = new WebTaskManager(this);
        //    webTaskManager.addTaskListener(this);

        //    tryToSendAlertBroadcast(IntentParamsBuilder.getActionAutoSyncSchedule());
        //    registerScreenEvent();

        //    Date today = DateUtils.getCurrentDate();
        //    if (getPreferencesHelper().getLastRepeatCheckTime() < today.getTime()) {
        //        // today not check
        //        sendDailyScheduleBroadcast();

        //    } else {
        //        // 我们要保证repeat task check 能正常运行
        //        DailyScheduleReceriver.scheduleNextDailyChecking(this);
        //    }

        //    registerReceiver(settingsChangedReceiver, settingsIntentFilter);
        //    tryToSendOnLaunchBroadcast();
        //    if (!FileUtils.hasExternalStoragePrivatePicture(this)) {
        //        FileUtils.createExternalStoragePrivatePicture(this);
        //    }
        //    if (CommentHelper.isFirstUse()) {
        //        CommentHelper.setFirstUseTime(System.currentTimeMillis());
        //    }

        //    // to prevent a weird bug where locale is null
        //    Configuration config = getResources().getConfiguration();
        //    if (config.locale == null)
        //        config.locale = Locale.getDefault();
        //}

        //@Override
        //protected void attachBaseContext(Context base) {
        //    super.attachBaseContext(base);
        //    MultiDex.install(this);
        //}

        //private void initDBServices() {
        //    taskService = new TaskService(dbHelper);
        //    attachmentService = new AttachmentService(dbHelper);
        //    checklistItemService = new ChecklistItemService(this);
        //    syncStatusService = new SyncStatusService(dbHelper);
        //    userProfileService = new UserProfileService(dbHelper);
        //    projectService = new ProjectService(this);
        //    checklistItemService = new ChecklistItemService(this);
        //    calendarProjectService = new CalendarProjectService(dbHelper);
        //    calendarSubscribeProfileService = new CalendarSubscribeProfileService(dbHelper);
        //    calendarEventService = new CalendarEventService(dbHelper);
        //    calendarReminderService = new CalendarReminderService(dbHelper);
        //    commentService = new CommentService(dbHelper);
        //}

        //public boolean isChinaMainland() {
        //    return "zh_CN".equals(getResources().getConfiguration().locale.toString());
        //}

        //public static String getVersion() {
        //    try {
        //        PackageInfo manager = staticApplication.getPackageManager().getPackageInfo(
        //                staticApplication.getPackageName(), 0);
        //        return manager.versionName;
        //    } catch (NameNotFoundException e) {
        //        return "Unknown";
        //    }
        //}

        //public TickTickDBHelper getDBHelper() {
        //    return dbHelper;
        //}

        //private ContentObserver calendarChangeObserver = new ContentObserver(null) {

        //    @Override
        //    public boolean deliverSelfNotifications() {
        //        return true;
        //    }

        //    @Override
        //    public void onChange(boolean selfChange) {
        //        super.onChange(selfChange);
        //        tryToSendBroadcast();
        //    }
        //};

        //private void registerScreenEvent() {
        //    IntentFilter filter = new IntentFilter(Intent.ACTION_SCREEN_ON);
        //    filter.addAction(Intent.ACTION_SCREEN_OFF);
        //    BroadcastReceiver mReceiver = new ScreenReceiver();
        //    registerReceiver(mReceiver, filter);
        //}

        //public ChecklistItemService getChecklistItemService() {
        //    return this.checklistItemService;
        //}

        //public String getDailyReminderTimeFlag() {
        //    UserProfile profile = getAccountManager().getCurrentUser().getUserProfile();
        //    return profile == null ? "-1" : profile.getDailyReminderTime();
        //}

        //@Override
        //public void onTerminate() {
        //    super.onTerminate();
        //    dbHelper.close();
        //    ContentResolver cr = getContentResolver();
        //    cr.unregisterContentObserver(calendarChangeObserver);
        //    webTaskManager.shutdown();
        //    getPreferencesHelper().release();
        //}

        //private TickTickAccountManager initAccountManager() {
        //    return new TickTickAccountManager(this);
        //}

        //private TickTickDBHelper initDBHelper() {
        //    return new TickTickDBHelper(this);
        //}

        //public void showMissReminderDialog() {
        //    handler.postDelayed(new Runnable() {

        //        @Override
        //        public void run() {
        //            Intent intent = new Intent(IntentParamsBuilder.getActionMissReminderShow());
        //            sendBroadcast(intent);
        //        }
        //    }, 500);
        //}

        ///**
        // * Send the on launch broadcast to location receiver
        // */
        //private void tryToSendOnLaunchBroadcast() {
        //    if (System.currentTimeMillis() - getPreferencesHelper().getLastOnLaunchTime()
        //            > android.text.format.DateUtils.HOUR_IN_MILLIS) {
        //        getPreferencesHelper().setLastOnLaunchTime(System.currentTimeMillis());
        //        handler.postDelayed(new Runnable() {

        //            @Override
        //            public void run() {
        //                Intent intent = new Intent(IntentParamsBuilder.getActionOnLaunch());
        //                sendBroadcast(intent);
        //            }
        //        }, 300);
        //    }
        //}

        //private void sendDailyScheduleBroadcast() {
        //    handler.postDelayed(new Runnable() {

        //        @Override
        //        public void run() {
        //            sendBroadcast(new Intent(IntentParamsBuilder.getActionDaily()));
        //        }
        //    }, 500);
        //}

        //public void sendLocationAlertChangedBroadcast() {
        //    Intent intent = new Intent(IntentParamsBuilder.getActionLocationAlertSchedule());
        //    sendBroadcast(intent);
        //}

        //public void sendLocationAlertChangedBroadcast(ArrayList<String> geofenceIds) {
        //    Intent intent = new Intent(IntentParamsBuilder.getActionLocationAlertSchedule());
        //    intent.putStringArrayListExtra(IntentExtraName.LOCATION_GEOFENCE_IDS, geofenceIds);
        //    sendBroadcast(intent);
        //}

        ///**
        // * Need to send Reminder changed broadcast if - 1. Task reminder time /
        // * completed changed 2. Tasklist deleted 3. Synchronized
        // */
        //public void sendTask2ReminderChangedBroadcast() {
        //    Intent intent = new Intent(IntentParamsBuilder.getActionReminderTimeChanged());
        //    sendBroadcast(intent);
        //}

        //public void sendNotificationOngoingBroadcast(long taskID) {
        //    sendNotificationOngoingBroadcast(taskID, false);
        //}

        //public void sendWearDataChangedBroadcast() {
        //}

        //public void sendCalendarEventChangeBroadcast() {
        //    Intent intent = new Intent(IntentParamsBuilder.getActionCalendarEventChanged());
        //    sendBroadcast(intent);
        //}

        ///**
        // * @param taskID      更新当前选中的Task，-1表示不更新当前选中
        // * @param dateChanged 数据更新
        // */
        //public void sendNotificationOngoingBroadcast(long taskID, boolean dateChanged) {
        //    if (getPreferencesHelper().isStatusBarShow()) {
        //        sendBroadcast(NotificationOngoing.createOnGoingIntent(taskID, dateChanged));
        //    }
        //}

        //public void tryToSendBroadcast() {
        //    if (isSendBroadcast) {
        //        // is Sending broadcast
        //        return;
        //    }
        //    isSendBroadcast = true;
        //    handler.postDelayed(new Runnable() {

        //        @Override
        //        public void run() {
        //            tryToSendWidgetTaskUpdateBroadcast();
        //            sendWearDataChangedBroadcast();
        //            sendNotificationOngoingBroadcast(-1);
        //            staticApplication.getContentResolver()
        //                    .notifyChange(UriBuilder.getProviderContentNotifyChangeUri(), null);
        //            isSendBroadcast = false;
        //        }
        //    }, 500);
        //}

        //public void tryToSendWidgetTaskUpdateBroadcast() {
        //    if (activeActivities == 0) {
        //        Intent intent = new Intent(IntentParamsBuilder.getActionTasksUpdated());
        //        sendBroadcast(intent);
        //        TickTickApplicationBase.this.getContentResolver().notifyChange(
        //                UriBuilder.getNewTaskContentUri(), null);
        //        Log.d(TAG, "broadcast task updated intent");
        //    }
        //}

        //public void sendNotificationDailySummaryBroadcast() {
        //    Intent intent = new Intent(IntentParamsBuilder.getActionNotificationDailySchedule());
        //    sendBroadcast(intent);
        //}

        //private final BroadcastReceiver settingsChangedReceiver = new BroadcastReceiver() {
        //    private volatile boolean isProviderEnable = false;
        //    private volatile boolean isInProcess = false;

        //    @Override
        //    public void onReceive(Context context, Intent intent) {
        //        final String action = intent.getAction();

        //        if (action.equals(Intent.ACTION_TIME_CHANGED)) {
        //            getPreferencesHelper().initTimeSettings();
        //        } else if (TextUtils.equals(action, "android.location.PROVIDERS_CHANGED")) {
        //            if (LocationUtils.isNetworkLocationProviderAvailiable(context)) {
        //                LocationLogger.logDebug("onProviderEnabled....");
        //                isProviderEnable = true;
        //                if (!isInProcess) {
        //                    isInProcess = true;
        //                    tryToResetLocationAlert();
        //                }
        //            } else {
        //                LocationLogger.logDebug("onProviderDisabled....");
        //                isProviderEnable = false;
        //            }
        //        }
        //    }

        //    private void tryToResetLocationAlert() {
        //        handler.postDelayed(new Runnable() {

        //            @Override
        //            public void run() {
        //                if (isProviderEnable) {
        //                    LocationLogger.logWarn("Network location is on, reset locaiton alert");
        //                    sendLocationAlertChangedBroadcast();
        //                    LocationUtils
        //                            .cancelNetworkAvailableNotification(TickTickApplicationBase.this);
        //                    isInProcess = false;
        //                }
        //            }
        //        }, android.text.format.DateUtils.SECOND_IN_MILLIS * 10);
        //    }
        //};

        //private AtomicBoolean isSyncInProceed = new AtomicBoolean(false);

        //public void tryToBackgroundSync() {
        //    try {
        //        if (!getAccountManager().isLocalMode()) {
        //            if (isSyncInProceed.get()) {
        //                return;
        //            }
        //            isSyncInProceed.set(true);
        //            handler.postDelayed(new Runnable() {

        //                @Override
        //                public void run() {
        //                    if (isSyncInProceed.get()) {
        //                        webTaskManager.syncQuietly();
        //                    }
        //                    isSyncInProceed.set(false);
        //                }
        //            }, android.text.format.DateUtils.SECOND_IN_MILLIS * 5);
        //        }
        //    } catch (Exception e) {
        //        Log.e(TAG, e.getMessage(), e);
        //    }
        //}

        //@Override
        //public void onSynchronized(SyncResult result) {
        //    if (activeActivities == 0 && result != null && result.hasChanged()) {
        //        if (result.isRemoteTaskChanged()) {
        //            Log.d(TAG, "tryToBackgroundSync..send schedule broadcast");
        //            sendLocationAlertChangedBroadcast();
        //            sendTask2ReminderChangedBroadcast();
        //            tryToSendBroadcast();
        //        }
        //    }
        //}

        ///**
        // * 后台定时同步
        // */
        //public void tryToSendAlertBroadcast(final String intentType) {
        //    handler.postDelayed(new Runnable() {
        //        @Override
        //        public void run() {
        //            Intent intent = new Intent(intentType);
        //            sendBroadcast(intent);
        //        }
        //    }, 500);
        //}

        ///**
        // * 闹钟schedule
        // */
        //private void tryToSendTaskAlertScheduleBroadcast() {
        //    handler.postDelayed(new Runnable() {

        //        @Override
        //        public void run() {
        //            sendTask2ReminderChangedBroadcast();
        //        }
        //    }, 500);
        //}

        //public void sendLocationAlertChangedBroadcast(String geofenceId) {
        //    ArrayList<String> geofenceIds = new ArrayList<String>();
        //    geofenceIds.add(geofenceId);
        //    sendLocationAlertChangedBroadcast(geofenceIds);
        //}

        //private void registerNetworkState() {
        //    IntentFilter filter = new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE");
        //    BroadcastReceiver networkReceiver = new BroadcastReceiver() {

        //        @Override
        //        public void onReceive(Context context, Intent intent) {
        //            checkNetworkState(context);
        //        }

        //    };
        //    registerReceiver(networkReceiver, filter);
        //}

        //private void checkNetworkState(Context context) {
        //    Log.d(TAG, "************** CheckNetwork ************");
        //    ConnectivityManager cmng = (ConnectivityManager) context
        //            .getSystemService(Context.CONNECTIVITY_SERVICE);
        //    if (cmng == null) {
        //        Log.e(TAG, "CONNECTIVITY_SERVICE not found");
        //        return;
        //    }
        //    NetworkInfo wifiNetInfo = cmng.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
        //    if (wifiNetInfo == null) {
        //        Log.e(TAG, "NetworkInfo not found");
        //        return;
        //    }
        //    if (wifiNetInfo.isConnected()) {
        //        Log.w(TAG, "wifi enable");
        //        isWifiEnable = true;
        //        isNetworkEnable = true;
        //    } else {
        //        isWifiEnable = false;
        //        NetworkInfo activeNetInfo = cmng.getActiveNetworkInfo();
        //        if (activeNetInfo != null) {
        //            Log.w(TAG, "wifi disable, network enable");
        //            isNetworkEnable = true;
        //        } else {
        //            Log.w(TAG, "wifi disable, network disable");
        //            isNetworkEnable = false;
        //        }
        //    }
        //}

        //public void setNeedResyncUserStatus(boolean needSync) {
        //    this.needResyncUserStatus = needSync;
        //    if (needSync) {
        //        getPreferencesHelper().setLastCheckStatusTime(0);
        //    }
        //}

        //public boolean isNeedResyncUserStatus() {
        //    return this.needResyncUserStatus;
        //}

        //public UserProfileService getUserProfileService() {
        //    return userProfileService;
        //}

        //public SettingsPreferencesHelper getPreferencesHelper() {
        //    if (preferencesHelper == null) {
        //        preferencesHelper = new SettingsPreferencesHelper(this);
        //        preferencesHelper.initSettings();
        //    }
        //    return preferencesHelper;
        //}

        //public static boolean isSdcardExist() {
        //    return Environment.MEDIA_MOUNTED.equals(Environment.getExternalStorageState())
        //            || !ImageCache.isExternalStorageRemovable();
        //}

        //public CalendarProjectService getCalendarProjectService() {
        //    return calendarProjectService;
        //}

        //public CalendarSubscribeProfileService getCalendarSubscribeProfileService() {
        //    return calendarSubscribeProfileService;
        //}

        //public CalendarEventService getCalendarEventService() {
        //    return calendarEventService;
        //}

        //public CalendarReminderService getCalendarReminderService() {
        //    return calendarReminderService;
        //}

        //public AnalyticsInstance getAnalyticsInstance() {
        //    return analyticsInstance;
        //}

        //protected abstract AnalyticsInstance initAnalyticsInstance();

        //public abstract AuthTokenTimeoutManagerBase getAuthTokenTimeoutManager();

        //public abstract PushManagerBase getPushManager();

        //public abstract VoiceHelperBase getVoiceHelper();

        //public abstract HttpUrlBuilderBase getHttpUrlBuilder();

        //public abstract SplashControllerBase getSplashControllerBase();

        //public CommentService getCommentService() {
        //    return commentService;
        //}
        #endregion
    }
}
