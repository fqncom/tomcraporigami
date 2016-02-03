using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Models;
using Windows.Networking.Connectivity;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TickTick.Helper
{
    public static class HttpHelper
    {
        //获取设备信息
        private static Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation DeviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
        /// <summary>
        /// 网络连接对象
        /// </summary>
        private static ConnectionProfile ConnectionProfile { get; set; }//NetworkInformation.GetInternetConnectionProfile();
        /// <summary>
        /// 检测是否连接网络
        /// </summary>
        public static bool IsConnectedToNetwork
        {
            get
            {
                ConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                if (ConnectionProfile != null)
                {

#if DEBUG
                    var interfaceType = ConnectionProfile.NetworkAdapter.IanaInterfaceType;
                    //return interfaceType == 71 || interfaceType == 6;
#endif
                    return ConnectionProfile.IsWlanConnectionProfile || ConnectionProfile.IsWwanConnectionProfile || interfaceType == 71 || interfaceType == 6;
                }
                return false;
            }
        }

        /// <summary>
        /// 发送请求从服务器获取对象
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<T> PostForObject<T>(Uri uri, object obj) where T : class,new()
        {
            //if (!IsConnectedToNetwork)
            //{
            //    // 最好在这里加入一个messageHelper，然后在前台读取这个，进行友好的提示
            //    return null;
            //}
            HttpClient httpClient = new HttpClient();
            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            //request.Headers.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            //HttpFormUrlEncodedContent postData = 

            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new HttpContentCodingWithQualityHeaderValue("charset=UTF-8"));
            //httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new HttpLanguageRangeWithQualityHeaderValue("zh_CN"));

            //httpClient.DefaultRequestHeaders.Add(new KeyValuePair<string, string>("Content-Type", "application/json"));
            //httpClient.DefaultRequestHeaders.Add(new KeyValuePair<string, string>("locale", "zh_CN"));
            ////设备信息
            //httpClient.DefaultRequestHeaders.Add(new KeyValuePair<string, string>("X-Device", string.Format("deviceName:{0},deviceManufacturer:{1},deviceFriendlyName:{2},deviceFirmwareVersion:{3},deviceHardwareVersion:{4},deviceSku:{5},deivceId:{6},deviceOperatingSystem:{7};", DeviceInfo.SystemProductName, DeviceInfo.SystemManufacturer, DeviceInfo.FriendlyName, DeviceInfo.SystemFirmwareVersion, DeviceInfo.SystemHardwareVersion, DeviceInfo.SystemSku, DeviceInfo.Id, DeviceInfo.OperatingSystem)));

            //iphone示例：X-Device: iPhone OS 8.3,iPhone,1930,6ef039dcff164c9c8ba571bfbd08a3c3,ios_iphone

            // "{\"devicename\":\"" + deviceInfo.SystemProductName + "\",\"deviceManufacturer\":\"" + deviceInfo.SystemManufacturer + ";"+deviceInfo.FriendlyName
            //     + ";" + deviceInfo.SystemFirmwareVersion + ";" + deviceInfo.SystemHardwareVersion + ";" + deviceInfo.SystemSku + ";"

            //序列化要发送到服务端的请求内容
            var ms = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings {  DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc, ContractResolver = new CamelCasePropertyNamesContractResolver() });

            //await LoggerHelper.LogToAllChannels(null, "登入发送账户信息");

            //发送请求并获取服务器端返回的数据
            HttpResponseMessage response = await httpClient.PostAsync(uri, new HttpStringContent(ms, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            //await LoggerHelper.LogToAllChannels(null, "登入接受到返回信息");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc });
            }
            return null;//此处有坑=========
        }
        /// <summary>
        /// 根据api拿数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<T> GetForObject<T>(Uri uri) where T : class,new()
        {
            string responseString = await GetForString(uri);
            //responseString = responseString.Replace("\"id\":", "\"sid\":");
            if (string.IsNullOrEmpty(responseString))
            {
                // TODO 优化，友好的提示
                return null;
            }
            return JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
        }
        /// <summary>
        /// 根据api拿字符串
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> GetForString(Uri uri)
        {
            if (!IsConnectedToNetwork)
            {
                // TODO 优化，友好的提示
                return string.Empty;
            }
            HttpClient httpClient = new HttpClient();

            var headers = httpClient.DefaultRequestHeaders;
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            HttpResponseMessage response = new HttpResponseMessage();
            response = await httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}
