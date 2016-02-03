using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TestDemo.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace TestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class JsonDemo : Page
    {
        public JsonDemo()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await UserSignOn("chenchao21@gmail.com", "111111");
            await HttpGet("https://ticktick.com/api/v2/batch/check/0");
        }
        public async Task UserSignOn(string userName, string userPwd)
        {
            HttpClient httpClient = new HttpClient();
            Uri posturi = new Uri("https://ticktick.com/api/v2/user/signon");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, posturi);

            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

            var ms = JsonConvert.SerializeObject(new { username = userName, password = userPwd });

            HttpResponseMessage response = await httpClient.PostAsync(posturi, new HttpStringContent(ms, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            var signUserInfo = JsonConvert.DeserializeObject<SignUserInfo>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {

            }
        }


        private static string responseString;
        public async Task HttpGet(string uri)
        {
            HttpClient httpClient = new HttpClient();

            var headers = httpClient.DefaultRequestHeaders;
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await httpClient.GetAsync(new Uri(uri, UriKind.Absolute));

                response.EnsureSuccessStatusCode();

                responseString = await response.Content.ReadAsStringAsync();

                var syncBean = JsonConvert.DeserializeObject<SyncBean>(responseString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("something wrong");
            }
        }


    }
}
