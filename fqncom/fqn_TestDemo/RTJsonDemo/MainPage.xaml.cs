using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Windows.Data.Json;
using System.IO;
using System.Text;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace RTJsonDemo
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

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void TestJsonDemo()
        {
            Person per = new Person { Id = 1, Name = "heh3" };
            var result = string.Empty;
            //序列化
            DataContractJsonSerializer js = new DataContractJsonSerializer(per.GetType());
            using (var stream = new MemoryStream())
            {
                js.WriteObject(stream, per);
                stream.Position = 0;//千万注意要将流归零
                using (var reader = new StreamReader(stream))
                {
                    System.Diagnostics.Debug.WriteLine(result = await reader.ReadToEndAsync());
                }
            }

            //反序列化
            DataContractJsonSerializer js2 = new DataContractJsonSerializer(typeof(Person));
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result)))
            {
                Person per2 = js2.ReadObject(stream) as Person;
                System.Diagnostics.Debug.WriteLine(per2.Name);
            }


            //System.Diagnostics.Debug.WriteLine(per.GetJson());

        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            TestJsonDemo();
        }
    }

    public class Person
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }


    public static class JsonObjectExt
    {
        public static string GetJson(this object obj)
        {
            var typeInfo = obj.GetType().GetTypeInfo();
            var props = typeInfo.DeclaredProperties;

            JsonObject jo = new JsonObject();

            foreach (var item in props)
            {
                var propTypeStr = item.PropertyType.ToString();//获取属性类型字符串，进行switch
                var propName = item.Name;//获取属性名称
                IJsonValue jsonValue = null;//精简代码。简单工厂
                switch (propTypeStr)
                {
                    case "System.String":
                        jsonValue = JsonValue.CreateStringValue(item.GetValue(obj).ToString());
                        break;
                    case "System.Double":
                        jsonValue = JsonValue.CreateNumberValue((double)item.GetValue(obj));
                        break;
                    case "System.Int32":
                        jsonValue = JsonValue.CreateNumberValue((int)item.GetValue(obj));
                        break;
                    case "System.Boolean":
                        jsonValue = JsonValue.CreateBooleanValue((bool)item.GetValue(obj));
                        break;
                    default:
                        break;
                }
                jo.SetNamedValue(propName, jsonValue);
            }
            return jo.Stringify();
        }
    }
}
