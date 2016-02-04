using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace YeildDemo
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
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }



        private string GetString(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in strs)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string[] strs = { "qwasdferqw", "qwerasdfqw", "qweasdfrqw", "qwerqwerw", "qwerqwerqw", "qwerqasfw", "qwerqzw", "qwersdfqw" };

            ////this.txtShow.Text = GetString(strs);

            //Person per = new Person() { Name = new string[] { "asdfas", "asdfasd", "asdfaswqew" } };


            ////由单步调试可以知道。foreach先拿到per，调用里面的GetEnuerator方法，将要遍历的属性通过一个包含有Current属性和MoveNext方法的一个对象的构造方法传递过去，然后执行in操作，in操作调用刚才对象的MoveNext方法，在这个方法中判断这个per是否还有元素没有遍历，如果是，则执行var item，接着去拿到这个对象的Current属性给item，最后再去对item进行操作。
            //foreach (var item in per)
            //{
            //    this.txtShow.Text = item.ToString();
            //}


            //yield 使用
            List<Person> list = new List<Person> { new Person { Id = 1 }, new Person { Id = 1 }, new Person { Id = 1 }, new Person { Id = 1 }, new Person { Id = 1 }, new Person { Id = 1 } };


            var list2 = GetPersonList(list);
            //此处存在问题，上面这一步走完之后，因为使用了yield所以list2此时为null，然后下方遍历的时候，根据foreach的原理，在调用IEnumerator的MoveNext方法的时候，会返回false，然后不会执行in操作，应该跳出foreach循环才对，但是实际上，在in操作执行之前，list2竟然从null变成了有具体的值了，而且更加诡异的是：这个时候根本还没有走下面的GetPersonList方法，也就是说目前为止还没有给List2赋值，但是他自己却有值了========（这是因为编译器帮我们做了一些操作，其实他不是null）。接着执行in操作的时候，去调用了GetPersonList方法，返回了一个值。奇怪！！奇怪！！！还有一个问题，因为yield是在需要值得时候才去取值，那么如果我的取值过程很繁琐（GetPersonList方法很复杂），那不是也会拖慢效率？是的，所以要看需求。
            foreach (var item in list2)
            {
                //int num0 = list2.FirstOrDefault();//num2
                int num = list2.LastOrDefault();//这一步我们发现，他只执行了一次GetPersonList方法，但是其中的foreach执行了很多次，直到找到需要的目标为止，然后返回，那么如果我不是顺序的去取值，而是乱序的去取，那么是不是会降低效率？
                int num2 = list2.FirstOrDefault();//从这里可以看出来，虽然我第一次进入foreach循环的时候已经取了一次第一个item，但是当我这里再去取一次第一个item的时候，他又要去执行一次GetPersonList方法。可见效率又低下去了。
                System.Diagnostics.Debug.WriteLine(item.ToString());

                //那么可以总结一下，使用yield的场景：当要取得数据不是那么的麻烦，而且对于一个集合来说是顺序取值，那么使用yield比较方便和高效（考虑到不是所有的集合元素都要遍历，只是遍历部分来说，比较高效）。。。。但是对于拿数据的过程比较繁琐，而且是乱序取值，且多次用同一个值的需求，那么再去使用yield来操作则显得不是那么高效了。
            }
        }

        private IEnumerable<int> GetPersonList(List<Person> list)
        {
            //List<int> list2 = new List<int>();
            foreach (var item in list)
            {
                yield return item.Id;
                //list2.Add(item);
            }
            //return list2;
        }
    }

    public class Person : IEnumerator
    {
        #region IEnumerator 成员

        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class Person
    {

        public int Id { get; set; }
        public string[] Name { get; set; }

        public MyEnumerator GetEnumerator()
        {
            return new MyEnumerator(this.Name);
        }
    }

    public class MyEnumerator
    {
        private string[] Obj;

        private int Index = -1;//用于标识索引位置
        public MyEnumerator(string[] obj)
        {
            // TODO: Complete member initialization
            this.Obj = obj;
        }

        public object Current { get { return this.Obj[this.Index]; } set { this.Obj[this.Index] = value.ToString(); } }

        public bool MoveNext() { return ++this.Index < this.Obj.Length; }//判断是否还有元素
    }
}
