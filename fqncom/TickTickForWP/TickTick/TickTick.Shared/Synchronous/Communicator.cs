using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Models;

namespace TickTick.Synchronous
{
    public class Communicator
    {
        #region 可抽象到父类的代码

        /// <summary>
        /// 主域地址
        /// </summary>
        private string SiteDomain = "https://dida365.com";
        //private HttpHelper HttpHelper;
        #endregion

        private static Communicator _instance;

        public static Communicator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock ("Communicator")
                    {
                        if (_instance == null)
                        {
                            return new Communicator();
                        }
                    }
                }
                return _instance;
            }
            //set { _instance = value; }
        }

        public Communicator()
        {
            //HttpHelper = new HttpHelper();

        }
        /// <summary>
        /// 根据api地址，返回对应Uri对象
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        private Uri GetUri(string api)
        {
            return new Uri(SiteDomain + api, UriKind.Absolute);
        }

        #region 登入登出
        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> SignOn(string userName, string userPwd)
        {
            return await HttpHelper.PostForObject<User>(GetUri("/api/v2/user/signon"), new { username = userName, password = userPwd });
        }
        #endregion

        #region 同步

        /// <summary>
        /// 首次获取同步数据SyncBean
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public async Task<SyncBean> BatchCheck(long point)
        {
            return await HttpHelper.GetForObject<SyncBean>(GetUri(string.Format("/api/v2/batch/check/{0}", point)));//此处“0”有坑============
        }
        /// <summary>
        /// 更新Task
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BatchUpdateResult> BatchUpdateTask(SyncTaskBean data)
        {
            return await HttpHelper.PostForObject<BatchUpdateResult>(GetUri("/api/v2/batch/task"), data);
            //return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/task"), data, 
            //  BatchUpdateResult.class, new Object[0]);
        }
        /// <summary>
        /// 更新Projects
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BatchUpdateResult> BatchUpdateProject(SyncProjectBean data)
        {
            return await HttpHelper.PostForObject<BatchUpdateResult>(GetUri("/api/v2/batch/project"), data);
            //return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/project"), data, 
            //BatchUpdateResult.class, new Object[0]);
        }
        /// <summary>
        /// 更新task和projects
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BatchUpdateResult> BatchUpdateTaskProject(MoveProject[] data)
        {
            return await HttpHelper.PostForObject<BatchUpdateResult>(GetUri("/api/v2/batch/taskProject"), data);
            //return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/taskProject"), data, 
            //  BatchUpdateResult.class, new Object[0]);
        }
        public async Task<BatchUpdateResult> BatchUpdateTaskSortOrder(TasksProjects[] tps)
        {
            return await HttpHelper.PostForObject<BatchUpdateResult>(GetUri("/api/v2/batch/taskProjectSortOrder"), tps);
            //return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/taskProjectSortOrder"), tps, 
            //  BatchUpdateResult.class, new Object[0]);
        }

        #endregion

        #region 用户信息
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAvatar()
        {
            return await HttpHelper.GetForString(GetUri("/api/v2/avatar/getUrl"));
            //return (String)getRestTemplate().getForObject(getUrl("/api/v2/avatar/getUrl"), String.class, new Object[0]);
        }
        /// <summary>
        /// 根据用户Id获取头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetAvatarByUserId(long userId)
        {
            return await HttpHelper.GetForString(GetUri(string.Format("/api/v2/avatar/{0}", userId)));
            //return (String)getRestTemplate().getForObject(getUrl("/api/v2/avatar/{userId}"), String.class, new Object[] {
            //  userId });
        }
        #endregion



        #region android接口代码参考
        /*
         *  private String siteDomain = "https://ticktick.com";

            public Communicator(AuthManager authManager, ApiCallExceptionHandler exceptionHandler)
            {
              super(authManager, exceptionHandler);
            }

            public SyncBean batchCheck(long point)
            {
              return (SyncBean)getRestTemplate().getForObject(getUrl("/api/v2/batch/check/{point}"),
                SyncBean.class, new Object[] { Long.valueOf(point) });
            }

            public BatchUpdateResult batchUpdateTask(SyncTaskBean data)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/task"), data,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchUpdateResult batchUpdateProject(SyncProjectBean data)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/project"), data,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchUpdateResult bactchUpdteProjectGroup(SyncProjectGroupBean data)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/projectGroup"), data,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchUpdateResult batchUpdateTaskProject(MoveProject[] data)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/taskProject"), data,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchUpdateResult batchUpdateTaskSortOrder(TaskProject[] tps)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/taskProjectSortOrder"), tps,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchTaskOrderUpdateResult batchUpdateTaskOrder(SyncTaskOrderBean data)
            {
              return (BatchTaskOrderUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/batch/taskOrder"), data,
                BatchTaskOrderUpdateResult.class, new Object[0]);
            }

            public SignUserInfo signOAuth2(String siteDomain, String accessToken)
            {
              SignUserInfo info = (SignUserInfo)getRestTemplate().getForObject(
                getUrl("/api/v2/user/sign/OAuth2?site={site}&accessToken={accessToken}"),
                SignUserInfo.class, new Object[] { siteDomain, accessToken });
              if ((info != null) && (info.getToken() != null) && (info.getToken().length() > 0)) {
                getAuthManager().updateToken(info.getToken());
              }
              return info;
            }

            public SignUserInfo signTwitter(String accessToken, String accessTokenSecret)
            {
              SignUserInfo info =
                (SignUserInfo)getRestTemplate().getForObject(
                getUrl("/api/v2/user/sign/twitter?accessToken={accessToken}&accessTokenSecret={accessTokenSecret}"),
                SignUserInfo.class, new Object[] { accessToken, accessTokenSecret });
              if ((info != null) && (info.getToken() != null) && (info.getToken().length() > 0)) {
                getAuthManager().updateToken(info.getToken());
              }
              return info;
            }

            public SignUserInfo signOAuth2CN(String siteDomain, String accessToken, String uId)
            {
              SignUserInfo info = (SignUserInfo)getRestTemplate().getForObject(
                getUrl("/api/v2/user/sign/OAuth2?site={site}&accessToken={accessToken}&uId={uId}"),
                SignUserInfo.class, new Object[] { siteDomain, accessToken, uId });
              if ((info != null) && (info.getToken() != null) && (info.getToken().length() > 0)) {
                getAuthManager().updateToken(info.getToken());
              }
              return info;
            }

            public SignUserInfo signon(String username, String password)
            {
              Map<String, String> data = new HashMap();
              data.put("username", username);
              data.put("password", password);
              SignUserInfo info = (SignUserInfo)getRestTemplate().postForObject(getUrl("/api/v2/user/signon"), data,
                SignUserInfo.class, new Object[0]);
              if ((info != null) && (info.getToken() != null) && (info.getToken().length() > 0)) {
                getAuthManager().updateToken(info.getToken());
              }
              return info;
            }

            public void signout()
            {
              getRestTemplate().getForEntity(getUrl("/api/v2/user/signout"), String.class, new Object[0]);
            }

            public SignUserInfo signup(String username, String password, String inviteCode)
            {
              Map<String, String> data = new HashMap();
              data.put("username", username);
              data.put("password", password);
              SignUserInfo info = (SignUserInfo)getRestTemplate().postForObject(
                getUrl("/api/v2/user/signup?invitecode={invitecode}"), data, SignUserInfo.class, new Object[] {
                inviteCode });
              if ((info != null) && (info.getToken() != null) && (info.getToken().length() > 0)) {
                getAuthManager().updateToken(info.getToken());
              }
              return info;
            }

            public boolean checkSuggestCn()
            {
              return ((Boolean)getRestTemplate().getForObject(getUrl("/api/v2/user/sign/suggestcn"), Boolean.class, new Object[0])).booleanValue();
            }

            public boolean checkAvailableBrotherSite(String username)
            {
              return ((Boolean)getRestTemplate().getForObject(
                getUrl("/api/v2/user/sign/available/brothersite?username={username}"),
                Boolean.class, new Object[] { username })).booleanValue();
            }

            public SignUserInfo getUserStatus()
            {
              SignUserInfo info = (SignUserInfo)getRestTemplate().getForObject(getUrl("/api/v2/user/status"),
                SignUserInfo.class, new Object[0]);
              return info;
            }

            public void resentVerifyEmail()
            {
              getRestTemplate().postForObject(getUrl("/api/v2/user/resentVerifyEmail"), null,
                String.class, new Object[0]);
            }

            public ApiResult changePassword(ChangePasswordData changePasswordData)
            {
              return (ApiResult)getRestTemplate().postForObject(getUrl("/api/v2/user/changePassword"),
                changePasswordData, ApiResult.class, new Object[0]);
            }

            public void updateEmail(String username, String password)
            {
              Map<String, String> data = new HashMap();
              data.put("username", username);
              data.put("password", password);
              getRestTemplate().put(getUrl("/api/v2/user/profile/email"), data, new Object[0]);
            }

            public LimitsConfig getLimitsConfig()
            {
              return (LimitsConfig)getRestTemplate().getForObject(getUrl("/api/v2/config/limits"), LimitsConfig.class, new Object[0]);
            }

            public Limits getLimitsByUser()
            {
              return (Limits)getRestTemplate().getForObject(getUrl("/api/v2/config/limitsByUser"), Limits.class, new Object[0]);
            }

            public Boolean isUnderQuota()
            {
              return (Boolean)getRestTemplate().getForObject(getUrl("/api/v1/attachment/isUnderQuota"),
                Boolean.class, new Object[0]);
            }

            public String getInviteCode()
            {
              String code = (String)getRestTemplate().getForObject(getUrl("/api/v2/user/signup/inviteCode"),
                String.class, new Object[0]);
              return code;
            }

            public List<ShareRecordUser> getProjectShareRecordUsers(String projectId)
            {
              ShareRecordUser[] shareUsers = (ShareRecordUser[])getRestTemplate().getForObject(
                getUrl("/api/v2/project/{projectId}/shares"), [Lcom.ticktick.task.model.ShareRecordUser.class, new Object[] { projectId });
              return Arrays.asList(shareUsers);
            }

            public ShareRecord shareProject(String projectId, String toUsername)
            {
              ShareRecord shareRecord = new ShareRecord();
              shareRecord.setToUsername(toUsername);
 
              return (ShareRecord)getRestTemplate().postForObject(getUrl("/api/v2/project/{projectId}/share"),
                shareRecord, ShareRecord.class, new Object[] { projectId });
            }

            public void deleteProjectShare(String projectId, String recordId)
            {
              getRestTemplate().delete(getUrl("/api/v2/project/{projectId}/share/{recordId}"), new Object[] { projectId,
                recordId });
            }

            public List<Notification> getNotification()
            {
              Notification[] notifications = (Notification[])getRestTemplate().getForObject(
                getUrl("/api/v2/notification"), [Lcom.ticktick.task.entity.Notification.class, new Object[0]);
              return Arrays.asList(notifications);
            }

            public int getNotificationCount()
            {
              return ((Integer)getRestTemplate().getForObject(getUrl("/api/v2/notification/count"), Integer.class, new Object[0])).intValue();
            }

            public void acceptProjectShare(String projectId, String notificationId, int actionStatus)
            {
              getRestTemplate().postForObject(
                getUrl("/api/v2/project/{projectId}/share/accept/{notificationId}?actionStatus={actionStatus}"),
                null, String.class, new Object[] { projectId, notificationId, Integer.valueOf(actionStatus) });
            }

            public String getAutoSignOnToken()
            {
              ApiResult ar = (ApiResult)getRestTemplate().getForObject(getUrl("/api/v1/user/requestSignOnToken"),
                ApiResult.class, new Object[0]);
              if ((ar != null) && (ar.get("token") != null)) {
                return ar.get("token").toString();
              }
              return "";
            }

            protected String getUrl(String path)
            {
              return this.siteDomain + path;
            }

            public void setSiteDomain(String siteDomain)
            {
              this.siteDomain = siteDomain;
            }

            public UserPreference getUserSettings()
            {
              return (UserPreference)getRestTemplate().getForObject(getUrl("/api/v2/user/preferences/settings"),
                UserPreference.class, new Object[0]);
            }

            public void updateUserSettings(UserPreference userPreference)
            {
              getRestTemplate().put(getUrl("/api/v2/user/preferences/settings/"), userPreference, new Object[0]);
            }

            public void updateUserFakedUsername(String username, String password)
            {
              Map<String, String> user = new HashMap();
              user.put("username", username);
              user.put("password", password);
              getRestTemplate().put(getUrl("/api/v2/user/profile/fakedUsername"), user, new Object[0]);
            }

            public PushDevice registerPushDevice(PushDevice pd)
            {
              return (PushDevice)getRestTemplate().postForObject(getUrl("/api/v2/push/register"), pd,
                PushDevice.class, new Object[0]);
            }

            public void unregisterPushDevice(String id)
            {
              getRestTemplate().delete(getUrl("/api/v2/push/unregister/{id}"), new Object[] { id });
            }

            public FavoriteLocation createFavLocation(FavoriteLocation favLocation)
            {
              return (FavoriteLocation)getRestTemplate().postForObject(getUrl("/api/v2/user/favLocation"), favLocation,
                FavoriteLocation.class, new Object[0]);
            }

            public List<FavoriteLocation> getUserFavLocations()
            {
              FavoriteLocation[] favLocations = (FavoriteLocation[])getRestTemplate().getForObject(
                getUrl("/api/v2/user/favLocation"), [Lcom.ticktick.task.entity.FavoriteLocation.class, new Object[0]);
              return Arrays.asList(favLocations);
            }

            public FavoriteLocation updateUserFavLocation(FavoriteLocation favLocation)
            {
              return (FavoriteLocation)getRestTemplate().postForObject(getUrl("/api/v2/user/favLocation/{locationId}"),
                favLocation, FavoriteLocation.class, new Object[] { favLocation.getId() });
            }

            public void deleteFavLocation(String locationId)
            {
              getRestTemplate().delete(getUrl("/api/v2/user/favLocation/{locationId}"), new Object[] { locationId });
            }

            public TaskPagination getAllCompletedTasks(int start, int limit)
            {
              return (TaskPagination)getRestTemplate().getForObject(
                getUrl("/api/v2/project/all/completed/pagination?start={start}&limit={limit}"),
                TaskPagination.class, new Object[] { Integer.valueOf(start), Integer.valueOf(limit) });
            }

            public TaskPagination getCompletedTasks(String id, int start, int limit)
            {
              return (TaskPagination)getRestTemplate().getForObject(
                getUrl("/api/v2/project/{id}/completed/pagination?start={start}&limit={limit}"),
                TaskPagination.class, new Object[] { id, Integer.valueOf(start), Integer.valueOf(limit) });
            }

            public List<Task> getAllCompletedTasks(Date from, Date to, int limit)
            {
              String fromStr = from == null ? null : DateUtil.dateToString(from, "yyyy-MM-dd HH:mm:ss",
                TimeZone.getTimeZone("UTC"));
              String toStr = to == null ? null : DateUtil.dateToString(to, "yyyy-MM-dd HH:mm:ss",
                TimeZone.getTimeZone("UTC"));
              Task[] tasks = (Task[])getRestTemplate().getForObject(
                getUrl("/api/v2/project/all/completed?from={from}&to={to}&limit={limit}"),
                [Lcom.ticktick.task.entity.Task.class, new Object[] { fromStr, toStr, Integer.valueOf(limit) });
              return Arrays.asList(tasks);
            }

            public List<Task> getCompletedTasks(String ids, Date from, Date to, int limit)
            {
              String fromStr = from == null ? null : DateUtil.dateToString(from, "yyyy-MM-dd HH:mm:ss",
                TimeZone.getTimeZone("UTC"));
              String toStr = to == null ? null : DateUtil.dateToString(to, "yyyy-MM-dd HH:mm:ss",
                TimeZone.getTimeZone("UTC"));
              Task[] tasks = (Task[])getRestTemplate().getForObject(
                getUrl("/api/v2/project/{ids}/completed?from={from}&to={to}&limit={limit}"),
                [Lcom.ticktick.task.entity.Task.class, new Object[] { ids, fromStr, toStr, Integer.valueOf(limit) });
              return Arrays.asList(tasks);
            }

            public Attachment uploadAttachment(String projectId, String taskId, String attachmentId, File file)
            {
              MultiValueMap<String, Object> parts = new LinkedMultiValueMap();
              parts.add("name", file.getName());
              parts.add("file", new FileSystemResource(file));
              return (Attachment)getRestTemplate().postForObject(
                getUrl("/api/v1/attachment/upload/{projectId}/{taskId}/{attachmentId}"), parts,
                Attachment.class, new Object[] { projectId, taskId, attachmentId });
            }

            public void getAttachment(String projectId, String taskId, String attachmentId, FileResponseExtractor responseExtractor)
            {
              getRestTemplate().execute(getUrl("/api/v1/attachment/{projectId}/{taskId}/{attachmentId}"),
                HttpMethod.GET, null, responseExtractor, new Object[] { projectId, taskId, attachmentId });
            }

            public void deleteAttachment(String projectId, String taskId, String attachmentId)
            {
              getRestTemplate().delete(getUrl("/api/v1/attachment/{projectId}/{taskId}/{attachmentId}"), new Object[] {
                projectId, taskId, attachmentId });
            }

            public String getAlipayInfo(String freq, int count)
            {
              return (String)getRestTemplate().getForObject(
                getUrl("/api/v1/payment/alipay_android?freq={freq}&count={count}"), String.class, new Object[] {
                freq, Integer.valueOf(count) });
            }

            public Ranking getRanking()
            {
              return (Ranking)getRestTemplate().getForObject(getUrl("/api/v2/user/ranking"), Ranking.class, new Object[0]);
            }

            public Boolean uploadAvatar(File file)
            {
              MultiValueMap<String, Object> parts = new LinkedMultiValueMap();
              parts.add("file", new FileSystemResource(file));
              return (Boolean)getRestTemplate().postForObject(getUrl("/api/v1/avatar"), parts, Boolean.class, new Object[0]);
            }

            public String getAvatar()
            {
              return (String)getRestTemplate().getForObject(getUrl("/api/v2/avatar/getUrl"), String.class, new Object[0]);
            }

            public String getAvatarByUserId(Long userId)
            {
              return (String)getRestTemplate().getForObject(getUrl("/api/v2/avatar/{userId}"), String.class, new Object[] {
                userId });
            }

            public String getAvatarByUserCode(String userCode)
            {
              return (String)getRestTemplate().getForObject(getUrl("/pub/api/v2/avatar/{userCode}"),
                String.class, new Object[] { userCode });
            }

            public User getUserProfile()
            {
              return (User)getRestTemplate().getForObject(getUrl("/api/v2/user/profile"), User.class, new Object[0]);
            }

            public void closeProject(String projectId)
            {
              getRestTemplate().put(getUrl("/api/v2/project/{projectId}/close"), projectId, new Object[0]);
            }

            public List<Task> getTasksByProject(String projectId)
            {
              Task[] tasks = (Task[])getRestTemplate().getForObject(getUrl("/api/v2/project/{id}/tasks"),
                [Lcom.ticktick.task.entity.Task.class, new Object[] { projectId });
              return Arrays.asList(tasks);
            }

            public List<TaskEtag> getTasksEtagByProject(String projectId)
            {
              TaskEtag[] tasks = (TaskEtag[])getRestTemplate().getForObject(getUrl("/api/v2/project/{id}/taskEtags"),
                [Lcom.ticktick.task.model.task.TaskEtag.class, new Object[] { projectId });
              return Arrays.asList(tasks);
            }

            public Task getTask(String projectId, String taskId)
            {
              return (Task)getRestTemplate().getForObject(
                getUrl("/api/v2/task/{taskId}?projectId={projectId}"), Task.class, new Object[] { taskId,
                projectId });
            }

            public List<ProjectProfile> getProjects()
            {
              ProjectProfile[] projects = (ProjectProfile[])getRestTemplate().getForObject(getUrl("/api/v2/projects"),
                [Lcom.ticktick.task.entity.ProjectProfile.class, new Object[0]);
              return Arrays.asList(projects);
            }

            public List<OrderSpecification> getOrderSpecifications()
            {
              OrderSpecification[] specs = (OrderSpecification[])getRestTemplate().getForObject(
                getUrl("/api/v1/payment/order_spec"), [Lorg.dayup.payment.model.OrderSpecification.class, new Object[0]);
              return Arrays.asList(specs);
            }

            public SubscriptionInfo verifyGoogleSubscription(String packageName, String productId, String token, String baseOrderId)
            {
              Map<String, String> data = new HashMap();
              data.put("baseOrderId", baseOrderId);
              data.put("packageName", packageName);
              data.put("productId", productId);
              data.put("token", token);
              return (SubscriptionInfo)getRestTemplate().postForObject(getUrl("/api/v2/subscribe/verify/google"), data,
                SubscriptionInfo.class, new Object[0]);
            }

            public List<SubscriptionSpecification> getSubscriptionSpecifications()
            {
              SubscriptionSpecification[] specs = (SubscriptionSpecification[])getRestTemplate().getForObject(
                getUrl("/api/v2/subscribe/subscribe_spec"), [Lorg.dayup.payment.model.SubscriptionSpecification.class, new Object[0]);
              return Arrays.asList(specs);
            }

            public UserShareContacts getUserShareContacts()
            {
              return (UserShareContacts)getRestTemplate().getForObject(getUrl("/api/v2/share/shareContacts"),
                UserShareContacts.class, new Object[0]);
            }

            public CalendarSubscribeProfile subscribeCalendar(CalendarSubscribeProfile profile)
            {
              return (CalendarSubscribeProfile)getRestTemplate().postForObject(getUrl("/api/v2/calendar/subscribe"), profile,
                CalendarSubscribeProfile.class, new Object[0]);
            }

            public void unsubscribeCalendar(String id)
            {
              getRestTemplate().delete(getUrl("/api/v2/calendar/unsubscribe/{id}"), new Object[] { id });
            }

            public List<CalendarSubscribeProfile> getSubscriptionCalendar()
            {
              return Arrays.asList((CalendarSubscribeProfile[])getRestTemplate().getForObject(
                getUrl("/api/v2/calendar/subscription"), [Lcom.ticktick.task.entity.calendar.CalendarSubscribeProfile.class, new Object[0]));
            }

            public List<Comment> getComments(String projectId, String taskId)
            {
              return Arrays.asList((Comment[])getRestTemplate().getForObject(
                getUrl("/api/v2/task/{taskId}/comments?projectId={projectId}"), [Lcom.ticktick.task.entity.task.Comment.class, new Object[] {
                taskId, projectId }));
            }

            public void addComment(String projectId, String taskId, Comment comment)
            {
              getRestTemplate().postForLocation(
                getUrl("/api/v2/task/{taskId}/comment?projectId={projectId}"), comment, new Object[] { taskId,
                projectId });
            }

            public void updateCommment(String projectId, String taskId, Comment comment)
            {
              getRestTemplate().put(
                getUrl("/api/v2/task/{taskId}/comment/{commentId}?projectId={projectId}"), comment, new Object[] {
                taskId, comment.getId(), projectId });
            }

            public void deleteComment(String projectId, String taskId, String commentId)
            {
              getRestTemplate().delete(
                getUrl("/api/v2/task/{taskId}/comment/{commentId}?projectId={projectId}"), new Object[] { taskId,
                commentId, projectId });
            }

            public BatchUpdateResult updateAssignee(List<Assignment> assignList)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/task/assign"), assignList,
                BatchUpdateResult.class, new Object[0]);
            }

            public void updateName(User user)
            {
              getRestTemplate().put(getUrl("/api/v2/user/profile/name"), user, new Object[0]);
            }

            public TaskPagination getAllDeletedTasksByPagination(int start, int limit)
            {
              return (TaskPagination)getRestTemplate().getForObject(
                getUrl("/api/v2/project/all/trash/pagination?start={start}&limit={limit}"),
                TaskPagination.class, new Object[] { Integer.valueOf(start), Integer.valueOf(limit) });
            }

            public BatchUpdateResult batchRestoreDeletedTasks(MoveProject[] moveProjects)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(getUrl("/api/v2/trash/restore"), moveProjects,
                BatchUpdateResult.class, new Object[0]);
            }

            public BatchUpdateResult deleteTask(Boolean deleteforever, TaskProject[] taskProjects)
            {
              return (BatchUpdateResult)getRestTemplate().postForObject(
                getUrl("/api/v2/tasks/delete?forever={deleteforever}"), taskProjects,
                BatchUpdateResult.class, new Object[] { deleteforever });
            }

            public Promotion queryPromotion(PromotionRequestParam request)
            {
              return (Promotion)getRestTemplate().postForObject(getUrl("/pub/api/v1/promo/query"), request,
                Promotion.class, new Object[0]);
            }

            public void deleteTrashForever()
            {
              getRestTemplate().delete(getUrl("/api/v2/trash/empty"), new Object[0]);
            }

            public List<EventCategory> getCategroies()
            {
              return Arrays.asList((EventCategory[])getRestTemplate().getForObject(
                getUrl("/pub/api/v3/explore/categories"), [Lcom.ticktick.explore.entity.EventCategory.class, new Object[0]));
            }

            public List<SocialEvent> getSocialEvents(String categoryId, int page)
            {
              return Arrays.asList((SocialEvent[])getRestTemplate().getForObject(
                getUrl("/pub/api/v3/explore/categories/{categoryId}?page={page}"),
                [Lcom.ticktick.explore.entity.SocialEvent.class, new Object[] { categoryId, Integer.valueOf(page) }));
            }

            public SocialEvent getSocialEvent(String eventId)
            {
              return (SocialEvent)getRestTemplate().getForObject(getUrl("/pub/api/v3/explore/events/{eventId}"),
                SocialEvent.class, new Object[] { eventId });
            }

            public List<UserSubscribeEvent> getSubscribeEvents()
            {
              return Arrays.asList((UserSubscribeEvent[])getRestTemplate().getForObject(
                getUrl("/api/v3/explore/subscriptions"), [Lcom.ticktick.explore.entity.UserSubscribeEvent.class, new Object[0]));
            }

            public UserSubscribeEvent subscribeEvent(String eventId)
            {
              return (UserSubscribeEvent)getRestTemplate().postForObject(
                getUrl("/api/v3/explore/events/{eventId}/subscribe"), null,
                UserSubscribeEvent.class, new Object[] { eventId });
            }

            public void unsubscribeEvent(String subscribeId)
            {
              getRestTemplate().delete(getUrl("/api/v3/explore/subscriptions/{subscribeId}/unsubscribe"), new Object[] {
                subscribeId });
            }

            public void batchUnsubscribeEvent(String[] subscribeIds)
            {
              getRestTemplate().put(getUrl("/api/v3/explore/subscriptions/batch/unsubscribe"),
                subscribeIds, new Object[0]);
            }

            public UserSubscribeEvent getSubscribeEvent(String eventId)
            {
              return (UserSubscribeEvent)getRestTemplate().getForObject(
                getUrl("/api/v3/explore/events/{eventId}/subscriptions"), UserSubscribeEvent.class, new Object[] {
                eventId });
            }

            public void modifiedReminder(String subscribeId, String reminder)
            {
              getRestTemplate().put(getUrl("/api/v3/explore/subscriptions/{subscribeId}/modifiedReminder?reminder={reminder}"),
                null, new Object[] { subscribeId, reminder });
            }

            public Boolean countGPlayCampaign(GPlayCampaignData data)
            {
              return (Boolean)getRestTemplate().postForObject(getUrl("/pub/api/v1/stats/google_play"), data,
                Boolean.class, new Object[0]);
            }

            public List<UserPublicProfile> getUserPublicProfiles(String[] userCodes)
            {
              return Arrays.asList((UserPublicProfile[])getRestTemplate().postForObject(
                getUrl("/pub/api/v2/userPublicProfiles"), userCodes, [Lorg.dayup.common.model.UserPublicProfile.class, new Object[0]));
            }

            public void bindingThirdUser(int siteId, String openId, String accessToken)
            {
              getRestTemplate().postForObject(
                getUrl("/api/v2/user/wechat/binding?siteId={siteId}&openId={openId}&accessToken={accessToken}"),
                null, String.class, new Object[] { Integer.valueOf(siteId), openId, accessToken });
            }

            public UserBindingInfo getBindingInfo()
            {
              return (UserBindingInfo)getRestTemplate().getForObject(getUrl("/api/v2/user/userBindingInfo"),
                UserBindingInfo.class, new Object[0]);
            }

            public WechatUserProfile getWechatUserInfo()
            {
              return (WechatUserProfile)getRestTemplate().getForObject(getUrl("/api/v2/user/wechatUser"),
                WechatUserProfile.class, new Object[0]);
            }

            public ProjectInviteCollaborationResult createInviteProjectCollaboration(String projectId)
            {
              return (ProjectInviteCollaborationResult)getRestTemplate().postForObject(
                getUrl("/api/v2/project/{projectId}/collaboration/invite"), null,
                ProjectInviteCollaborationResult.class, new Object[] { projectId });
            }

            public ProjectInviteCollaborationResult getProjectInviteUrl(String projectId)
            {
              return (ProjectInviteCollaborationResult)getRestTemplate().getForObject(
                getUrl("/api/v2/project/{projectId}/collaboration/invite-url"),
                ProjectInviteCollaborationResult.class, new Object[] { projectId });
            }

            public ProjectApplyCollaborationResult applyJoinProject(String invitationId, String shareUserCode)
            {
              return
                (ProjectApplyCollaborationResult)getRestTemplate().postForObject(
                getUrl("/api/v2/project/collaboration/apply?invitationId={invitationId}&u={shareUserCode}"),
                null, ProjectApplyCollaborationResult.class, new Object[] { invitationId, shareUserCode });
            }

            public void acceptApplyJoinProject(String notificationId)
            {
              getRestTemplate().put(
                getUrl("/api/v2/project/collaboration/accept?notificationId={notificationId}"),
                null, new Object[] { notificationId });
            }

            public void refuseApplyJoinProject(String notificationId)
            {
              getRestTemplate().put(
                getUrl("/api/v2/project/collaboration/refuse?notificationId={notificationId}"),
                null, new Object[] { notificationId });
            }

            public void deleteShareContact(String toEmail)
            {
              getRestTemplate().delete(getUrl("/api/v2/share/shareContacts?toEmail={toEmail}"), new Object[] { toEmail });
            }
        }

        */
        #endregion


    }
}
