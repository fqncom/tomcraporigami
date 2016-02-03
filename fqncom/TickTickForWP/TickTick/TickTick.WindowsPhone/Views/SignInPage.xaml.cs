using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Models;
using TickTick.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TickTick.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public SignInPageViewModel ViewModel { get; set; }
        public UserBll UserBll = new UserBll();

        public SignInPage()
        {
            ViewModel = new SignInPageViewModel();
            this.InitializeComponent();

        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.SignUserInfo != null)
            {
                if (!string.Equals(App.SignUserInfo.Sid,User.LOCAL_MODE_ID) && App.SignUserInfo.IsLogOut)
                {
                    this.txtEmail.Text = App.SignUserInfo.UserName;
                }
            }

            this.pivotParent.SelectionChanged += (sender1, e1) =>
            {
                if (this.pivotParent.SelectedItem == this.pivotRegister)
                {
                    this.appBarSignIn.Icon = new SymbolIcon(Symbol.Add);
                    this.appBarSignIn.Label = "注册";
                    this.pivotParent.Items.Remove(this.pivotMain);
                    IsSignInType = false;
                }
                else if (this.pivotParent.SelectedItem == this.pivotSignIn)
                {
                    this.appBarSignIn.Icon = new SymbolIcon(Symbol.Contact);
                    this.appBarSignIn.Label = "登入";
                    this.pivotParent.Items.Remove(this.pivotMain);
                    IsSignInType = true;
                }
                else
                {

                }

            };


            this.pivotParent.Items.Remove(this.pivotRegister);
            this.pivotParent.Items.Remove(this.pivotSignIn);
            //this.pivotSignIn2.Opacity = 0;//.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //this.pivotMain.Items.Add(this.pivotSignIn2);

            //显示logo动画
            //this.sbShowLogo.Begin();
            //this.sbShowLogo.Completed += (sender1, e1) =>
            //{
            //    //当显示图片动画结束的时候，显示登入界面
            //    //干掉该元素
            //    //this.gridLogoShow.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //};
        }

        #region 弃用
        /// <summary>
        /// 点击确认登入按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void btnLoginConfirm_Clicked(object sender, TappedRoutedEventArgs e)
        //{
        //    //var beginTime = DateTime.UtcNow;

        //    //var userName = this.txtUsername.Text;
        //    //var userPwd = this.txtPassword.Password;
        //    //SignUserInfo userInfo = await ViewModel.Communicator.SignOn(userName, userPwd);

        //    //await LoggerHelper.LogToAllChannels(null, string.Format("用户登入成功，耗时：{0}", DateTime.UtcNow - beginTime));

        //    //if (userInfo != null)
        //    //{
        //    //    //登入成功则跳转到mainpage，此处有很多逻辑，离线状态，游客状态，许久未登入状态？
        //    //    var frame = Window.Current.Content as Frame;
        //    //    frame.Navigate(typeof(MainPage), userInfo);//适当的可以传一些参数过去
        //    //}


        //} 
        #endregion

        /// <summary>
        /// 当前logo是否是滴答logo
        /// </summary>
        private bool IsDida365Logo = true;
        /// <summary>
        /// 当前是登入操作还是注册操作
        /// </summary>
        private bool IsSignInType = true;

        private void btnSwapLogo_Clicked(object sender, RoutedEventArgs e)
        {
            if (IsDida365Logo)
            {
                this.imageLogo.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/dida365_logo.png"));
                this.btnSwap.Content = "切换至TickTick";
                IsDida365Logo = false;
            }
            else
            {
                this.imageLogo.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/ticktick_logo.png"));
                this.btnSwap.Content = "切换至滴答清单";
                IsDida365Logo = true;
            }
        }

        private void BtnSignIn_Clicked(object sender, TappedRoutedEventArgs e)
        {
            this.pivotParent.Items.Add(this.pivotSignIn);
            this.pivotParent.Items.Add(this.pivotRegister);
            this.pivotParent.SelectedItem = this.pivotSignIn;
            IsSignInType = true;
        }

        private void BtnRegister_Clicked(object sender, TappedRoutedEventArgs e)
        {
            this.pivotParent.Items.Add(this.pivotSignIn);
            this.pivotParent.Items.Add(this.pivotRegister);
            this.pivotParent.SelectedItem = this.pivotRegister;
            IsSignInType = false;
        }

        private void BackTest_Clicked(object sender, RoutedEventArgs e)
        {
            this.pivotParent.Items.Remove(this.pivotSignIn);
            this.pivotParent.Items.Remove(this.pivotRegister);
            this.pivotParent.Items.Add(this.pivotMain);
            this.pivotParent.SelectedItem = this.pivotMain;
        }

        private async void SignInOrRegister_Clicked(object sender, RoutedEventArgs e)
        {
            var email = this.txtEmail.Text;
            var password = this.txtPassword.Password;
            // TODO 基本判空操作,勿忘
            if (string.IsNullOrEmpty(email))
            {
                await MessageDialogHelper.MessageDialogShowAsync("邮箱不能为空", "提示");
                return;
            }
            if (!RegexHelper.IsMatchEmail(email))
            {
                await MessageDialogHelper.MessageDialogShowAsync("邮箱格式错误", "提示");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                await MessageDialogHelper.MessageDialogShowAsync("密码不能为空", "提示");
                return;
            }

            //判断是要登入还是注册
            //SignUserInfo userInfo = new SignUserInfo();

            //判断网络状态
            if (HttpHelper.IsConnectedToNetwork)
            {

            }
            if (IsSignInType)
            {
                //判断是滴答还是ticktick
                if (IsDida365Logo)
                {
                    //滴答状态
                    ViewModel.SignUserInfo = await ViewModel.Communicator.SignOn(email, password);
                }
                else
                {
                    //ticktick状态
                }
            }
            else
            {
                //注册操作
            }
            //再次判断网络状态
            if (true)
            {

            }
            if (ViewModel.SignUserInfo == null)
            {
                //用户不存在，或者密码错误
                await MessageDialogHelper.MessageDialogShowAsync("用户不存在，或者密码错误，可以使用临时用户登入", "提示");
                return;
            }
            App.SignUserInfo = ViewModel.SignUserInfo;
            //更新用户登入的时间
            await UserBll.UpdateUserLoginTime(App.SignUserInfo);
            //登入成功则跳转到mainpage，此处有很多逻辑，离线状态，游客状态，许久未登入状态？
            //var frame = Window.Current.Content as Frame;
            //frame.Navigate(typeof(MainPage), ViewModel.SignUserInfo);//适当的可以传一些参数过去
            NavigateHelper.NavigateToPage(typeof(MainPage));
        }
    }
}
