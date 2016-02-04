using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace xml序列化
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> perList = new List<Person>();
            perList.Add(new Person() { Name = "小明", Age = 15, Address = "浙江" });
            perList.Add(new Person() { Name = "小章", Age = 15, Address = "北京" });
            perList.Add(new Person() { Name = "小李", Age = 15, Address = "黑龙江" });

            #region xml序列化，将person对象按照xml数据格式保存

            //XmlSerializer xmls = new XmlSerializer(typeof(List<Person>));
            //using (FileStream fs = new FileStream("序列化.txt", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    xmls.Serialize(fs, perList);
            //}
            //Console.WriteLine("ok"); 

            #endregion

            #region javascript序列化，得到的结果是对象按照json数据格式保存,引用System.Web.Extensions

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //File.WriteAllText("json.txt", jss.Serialize(perList));

            //Console.WriteLine("ok");
            #endregion

            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
