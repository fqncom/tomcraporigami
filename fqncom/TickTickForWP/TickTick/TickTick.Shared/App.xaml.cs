using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WindowsUniversalLogger.Interfaces;
using WindowsUniversalLogger.Interfaces.Channels;
using WindowsUniversalLogger.Logging;
using WindowsUniversalLogger.Logging.Channels;
using WindowsUniversalLogger.Logging.Sessions;

// 有关“空白应用程序”模板的信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=234227

namespace TickTick
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    public sealed partial class App : Application
    {
        #region 自定义属性
        public static SQLiteAsyncConnection Connection { get; set; }
        public static User SignUserInfo { get; set; }
        public static UserBll UserBll = new UserBll();
        #endregion

#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {

            this.InitializeComponent();
            this.UnhandledException += async (sender1, e1) =>
            {
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(LogLevel.ERROR, string.Format("程序出错【{0}】", e1.Exception), DateTime.Now);
                }
            };
            this.Suspending += this.OnSuspending;
            // 注册后退按钮事件
            RegisterBackPressed();
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 当启动应用程序以打开特定的文件或显示搜索结果等操作时，
        /// 将使用其他入口点。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            #region 创建数据库
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync("TickTick.db");
                Connection = new SQLiteAsyncConnection("TickTick.db");
            }
            catch (FileNotFoundException)
            {
                DataService.CreateDbAsync();
            }
            #endregion

            #region 使用WindowsUniversalLogger进行日志处理

            ILoggingSession session = LoggingSession.Instance;
            ILoggingChannel channel = new FileLoggingChannel(
                "UniqueChannelName",
                ApplicationData.Current.LocalFolder,
                "logs_for_TickTick.txt");
            await channel.Init();
            session.AddLoggingChannel(channel);

            if (LoggerHelper.IS_LOG_ENABLED)
            {
                await LoggerHelper.LogToAllChannels(LogLevel.INFO, "程序启动", DateTime.Now);
            }
            #endregion

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                // TODO: 将此值更改为适合您的应用程序的缓存大小
                rootFrame.CacheSize = 3;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            #region 从数据库读取用户，如果存在，则直接赋值给全局变量，否则创建临时用户
            var defaultSignUserInfo = User.GetDefaultSignUserInfo();
            if (SignUserInfo == null || string.Equals(SignUserInfo.Sid, User.LOCAL_MODE_ID))
            {
                SignUserInfo = await UserBll.GetLocalLastSignUserInfo();
            }
            if (SignUserInfo == null)
            {
                //赋值本地默认用户，即临时用户
                SignUserInfo = defaultSignUserInfo;
            }
            #endregion

            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);



            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // 删除用于启动的旋转门导航。
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    //foreach (var c in rootFrame.ContentTransitions)
                    //{
                    //    this.transitions.Add(c);
                    //}
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif
                if (SignUserInfo.IsLogOut || SignUserInfo.Sid.Equals(User.LOCAL_MODE_ID))
                {
                    // 当未还原导航堆栈时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    if (!rootFrame.Navigate(typeof(SignInPage), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }

                else
                {
                    #region 判断是否是点击toast进入
                    var toastParam = e.Arguments;
                    if (!string.IsNullOrEmpty(toastParam))
                    {
                        if (toastParam.Contains("MainPage"))
                        {
                            if (!rootFrame.Navigate(typeof(MainPage), new FrameTransitionParam { TasksIdFromToast = Convert.ToInt32(toastParam.Substring(toastParam.IndexOf('=') + 1)) }))
                            {
                                throw new Exception("Failed to create initial page");
                            }
                            Window.Current.Activate();
                            return;
                        }
                    }
                    // 非临时用户
                    if (!string.Equals(SignUserInfo.Sid, User.LOCAL_MODE_ID))
                    {
                        if (!rootFrame.Navigate(typeof(MainPage)))
                        {
                            throw new Exception("Failed to create initial page");
                        }
                        Window.Current.Activate();
                        return;
                    }
                    #endregion
                }
            }

            // 确保当前窗口处于活动状态
            Window.Current.Activate();

        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// 启动应用程序后还原内容转换。
        /// </summary>
        /// <param name="sender">附加了处理程序的对象。</param>
        /// <param name="e">有关导航事件的详细信息。</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection();//{ new EdgeUIThemeTransition() };//{ new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// 在将要挂起应用程序执行时调用。    将保存应用程序状态
        /// 将被终止还是恢复的情况下保存应用程序状态，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起的请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        private static bool isExiting;
        /// <summary>
        /// 注册后退按钮事件
        /// </summary>
        private static void RegisterBackPressed()
        {
#if WINDOWS_PHONE_APP
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, e) =>
            {
                Frame root = Window.Current.Content as Frame;
                var page = root.Content as Page;
                if (page == null)
                {
                    return;
                }
                var container = page.Content as Windows.UI.Xaml.Controls.Panel;
                var tipElementsXml = @"<Border xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" x:Name=""exitTips"" Background=""#99000000"" HorizontalAlignment=""Center"" VerticalAlignment=""Center"" CornerRadius=""5"" Grid.RowSpan=""2"" Margin=""0,180,0,0"" Opacity=""0"">
    <Border.Resources>
        <Storyboard x:Name=""tipsFade"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName=""exitTips"" Storyboard.TargetProperty=""(UIElement.Opacity)"" AutoReverse=""False"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""0""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.5"" Value=""1""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:2.5"" Value=""1""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:3"" Value=""0""/>
            </DoubleAnimationUsingKeyFrames>
        </Storyboard>
    </Border.Resources>
    <TextBlock Text=""再按一次退出！"" Margin=""10,5"" FontSize=""26"" TextAlignment=""Center""/>
</Border>";
                Border border = Windows.UI.Xaml.Markup.XamlReader.Load(tipElementsXml) as Border;
                container.Children.Add(border);
                e.Handled = true;
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame.CanGoBack)
                {
                    rootFrame.GoBack();
                    return;
                }

                if (!isExiting)
                {
                    isExiting = true;
                    var storyboard = border.FindName("tipsFade") as Storyboard;
                    if (storyboard == null) return;
                    storyboard.Begin();
                    storyboard.Completed += (se, a) => { isExiting = false; container.Children.Remove(border); };
                }
                else
                {
                    App.Current.Exit();
                }
            };
#endif
        }
    }


}