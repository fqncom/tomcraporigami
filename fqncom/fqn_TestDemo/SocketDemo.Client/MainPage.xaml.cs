using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace SocketDemo.Client
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public StreamSocket socket;

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //建立一个链接
            HostName hostName = new HostName("192.168.28.24");

            socket = new StreamSocket();
            await socket.ConnectAsync(hostName, "10086");

            //将message读取到
            //string message = "this is come from .NET class";
            //获取长度
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = txtSend.Text;
            DataWriter writer = new DataWriter(socket.OutputStream);
            var length = writer.MeasureString(message);
            writer.WriteUInt32(length);
            //将字符串写入流中
            writer.WriteString(message);
            //向服务器发送数据
            await writer.StoreAsync();
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                textList.Items.Add(message);
            });
        }
    }
}
