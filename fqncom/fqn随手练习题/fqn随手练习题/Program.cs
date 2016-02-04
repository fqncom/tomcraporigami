using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _____fqn随手练习题
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 装箱和拆箱

            //返回false，因为两个是值类型，指向不同的地址。被装到了两个箱子里去
            //Console.WriteLine(object.ReferenceEquals(3, 3));

            //因为是引用类型，而且两个参数其实是一个参数，作为字符串，定义时，会放到一个队上的暂存池里面去，之后再定义相同的时候会先去暂存池里面去找，弱国找到一样的，则会将其在堆中的地址直接给新的对象，而不会在堆上开辟新的空间。
            //Console.WriteLine(object.ReferenceEquals("123","123"));

            //同上面的情况，定义str1的时候在堆上开辟空间存了“456”，然后在栈中保存了字符串的地址，然后当定义str2 的时候，会先去堆中找有没有一样的，然后发现有一样的，就直接将其地址给了str2，这样一来，str1和str2指向的地址是相同的。装箱时自然也就一样了
            //string str1 = "456";
            //string str2 = "456";
            //Console.WriteLine(object.ReferenceEquals(str1,str2));

            #endregion


            #region 装箱和拆箱2

            //int i = 2000;
            //object o = i;//装箱
            //i = 2001;
            //int j = (int)o;//拆箱
            //Console.WriteLine("i={0},o={1},j={2}", i, o, j);


            #endregion


            #region 值类型不能为null

            ////int i1 = null;
            //int? i2 = null;//值类型后加?就成了可空数据类型
            ////int i3 = i2;//所以把可空的赋值给一定不能为空的会报错。
            //int i4 = (int)i2;//可以显式转换，由程序员来保证“一定不为空”
            //int? i5 = i4;//一定会成功！
            //Console.WriteLine(i4);//会报错，显示i2必须有一个初始值
            //using()→try{]catch{}finally{}

            //int?是微软的一个语法糖。是一种和int没有直接关系的Nullable类型

            //Nullable<int> d1 = new Nullable<int>();//int? d1=null;
            //Nullable<int> d2 = new Nullable<int>(3);//int? d2=3;
            //Console.WriteLine(d1 == null);

            #endregion


            #region finally执行顺序

            ////方法定义在最后
            //Console.WriteLine(GetIt());

            #endregion


            #region 随机数问题

            //for (int i = 0; i < 10; i++)
            //{
            //    Random rand = new Random();
            //    ////输出结果基本一致，因为random内部是调用系统当前时间的毫秒数进行的计算，此for循环执行时间太短，系统时间基本一致。所以结果也基本一致
            //    Console.WriteLine(rand.Next(100));
            //}

            ////当将循环时间间隔调大时，就会明显看到返回的数据不一致了。
            //for (int i = 0; i < 100000; i++)
            //{
            //    Random rand = new Random();
            //    if (i % 5000 == 0)
            //    {
            //        Console.WriteLine(rand.Next(100));
            //    }
            //}

            #endregion


            #region 随机数问题2

            //int[] arr = new int[100];
            ////把100个数顺序放入
            //for (int i = 0; i < 100; i++)
            //{
            //    arr[i] = i + 1;
            //}

            //Random rand = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    //随机生成两个位置
            //    int m = rand.Next(0, 100);
            //    int n = rand.Next(0, 100);

            //    //颠倒两个位置
            //    int temp = arr[m];
            //    arr[m] = arr[n];
            //    arr[n] = temp;
            //    //Console.WriteLine(arr[i]);//生成随机数，随机调换两者的数据
            //}

            #endregion


            #region 斐波那契数列

            //Console.WriteLine(Fiber(30));
            //Console.WriteLine(Fiber2(30));

            #endregion


            #region 冒泡排序

            //int[] num = { 12, 23, 21, 56, 54, 89, 656, 23, 21, 23 };
            ////i表示的是还需要执行的次数，因为已经一次次的把最大的放到最后去了，所以次数会一次次减少
            //for (int i = 0; i < num.Length; i++)
            //{
            //    //一步步的把最大的移到最后一个位置去，j表示的是位置
            //    //执行完这个循环之后，最大的数就跑到最后去了，然后i自增一，表示接下来还需要循环的次数减少一次
            //    for (int j = 0; j < num.Length - 1 - i; j++)
            //    {
            //        if (num[j + 1] < num[j])
            //        {
            //            int temp = num[j];
            //            num[j] = num[j + 1];
            //            num[j + 1] = temp;
            //        }
            //        //foreach (int item in num)
            //        //{
            //        //    Console.Write(item + " ");
            //        //}
            //        //Console.WriteLine();
            //    }
            //}
            //foreach (int item in num)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion


            #region 去除集合或者数组中的重复项，使用hashset

            //int[] num = { 12, 54, 6, 5, 8, 9, 6, 5, 4, 2, 3, 6, 5, 4, 2, };
            //HashSet<int> has = new HashSet<int>();
            ////hashset在添加的时候就已经去重了，一旦里面有重复的，就不再往里加重复的值
            //foreach (var item in num)
            //{
            //    has.Add(item);
            //}
            //foreach (var item in has)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion


            #region 线程休息与当前时间获取的变化

            //Person p1 = new Person();//1:00
            //Console.WriteLine(DateTime.Now);
            //Thread.Sleep(1000 * 10);
            //Console.WriteLine(DateTime.Now);
            //Console.WriteLine(p1.BirthDay);
            ////打印出来的是对象New出来的时间
            ////因为字段是对象被new出来的时候初始化的

            #endregion


            #region 字段的初始化--有点无法理解，静态构造方法？

            //Person p1 = new Person();//使用静态构造方法，将A自增了1，然后在赋给B之后才又自增了1；
            //Console.WriteLine(p1.B);//根据以上得到，是先赋值给了B之后，A才又自增了1，所以，这里得到的是B=31
            //Console.WriteLine(Person.A);//然后因为赋值给了B之后又自增了1，所以这里得到的是自增之后的值，即32
            //Console.WriteLine(p1.B);//这里的B已经存放在了内存中，所以，还是原来的那个B
            //Console.WriteLine(Person.A);//这里的A也还是原来的A，因为上一行代码中，B不是重新调用，而是从内存中去拿到的

            //Person p2 = new Person();//这里的构造方法执行时，已经不会再执行类里面的静态构造方法了，所以静态的A还是原来的值，即32；
            //Console.WriteLine(p2.B);//这里的B得到的值也是上一行中的A先将自己赋值给了B，然后A才进行的自增
            //Console.WriteLine(Person.A);//根据上一行可以知道，p2在第一次得到B时，先将A的值给了B，然后A自增了1，所以，这里的A的值是33；

            #endregion


            #region 静态构造方法

            //Class1 o1 = new Class1();//类定义中，静态构造只执行一次，然后在这里，在执行完静态构造方法之后执行公有构造方法，然后之后当构造第二个对象时。不再使用静态的构造方法，所以，到这一步，结果是2；
            //Class1 o2 = new Class1();//第二次构造对象，在这里不再使用静态的构造方法，直接执行公有的构造方法，然后到这一步，结果是3；
            //Console.WriteLine(Class1.count);//所以，输出3

            #endregion


            #region 多异常获取问题

            //try
            //{
            //}
            //catch (FileNotFoundException e1)
            //{
            //}
            //catch (Exception e2)
            //{
            //}
            //catch (IOException e3)   //显示上一个异常捕获语句catch已经将所有异常都能捕获到了，已经不需要这个catch语句了
            //{
            //}
            //catch
            //{
            //} 


            #endregion


            #region 测试list改变问题
            List<string> list = new List<string>();
            list.Add("asdfas");
            ChangeList(list);
            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
            #endregion


            Console.ReadKey();
        }

        //测试list改变问题
        private static void ChangeList(List<string> list)
        {
            List<string> list2 = list;//如果这里使用new创建了新的对象，则list的地址会改变，然后进行操作就不是操作原来的list对象了
            list2.Add("测试改变list");
            list = list2;
            //list.AddRange(list2);
        }

        //不使用递归调用实现斐波那契数列
        private static int Fiber2(int p)
        {
            if (p < 0)
            {
                throw new ArgumentException("输入的值不能小于0");
            }
            if (p == 0)
            {
                return 1;
            }
            else
            {
                int[] num = new int[p + 1];
                num[0] = 1;
                num[1] = 1;
                for (int i = 2; i <= p; i++)
                {
                    num[i] = num[i - 1] + num[i - 2];
                }
                return num[p];
            }
        }

        //递归调用,实现斐波那契数列
        private static int Fiber(int p)
        {
            if (p < 0)
            {
                throw new ArgumentException("输入的值不能小于0");
            }
            if (p == 0 || p == 1)
            {
                return 1;
            }
            else
            {
                return Fiber(p - 2) + Fiber(p - 1);
            }
        }

        //探究finally执行顺序的方法
        static string GetIt()
        {
            int i = 8;
            //string str = "没改吧？";
            Person per = new Person();
            per.name = "我没变";
            try
            {
                i++;
                Console.WriteLine("a");
                return per.name;// return比finally 先执行,所以先将i返回了出去,然后再去执行finally中的内容，此时因为已经将i返回出了方法体，所以在finally中对i的操作已经没有意义了     把返回值设定为i，然后“尽快”返回（没啥事就回去吧）
            }
            finally
            {
                Console.WriteLine("b");
                //str = "改了么？";//此处字符串也没改。是个问题。那如果是对象呢?//一样，因为前面已经返回出去了。
                per.name = "我变了";//就算是对象也不会改变，因为通过反编译可以看到，return的时候是将要返回的数据赋值给了一个变量（变量类型根据方法的返回值来定义），然后返回的是这个变量，已经和原来的变量无关了。//其实是之前的已经返回了，在返回一次就是变了的。
                i++;
            }
        }

    }

    //person类
    public class Person
    {
        public string name;

        public DateTime BirthDay = DateTime.Now;

        public static int A = 30;
        static Person()//静态构造函数在static字段初始化完成后执行
        {//静态构造函数只执行一次
            A++;
        }
        //字段的初始化赋值代码只是在new一个对象的时候执行，而不是每次用字段的时候都执行
        public int B = A++;//这里，先将b返回了。返回之后然后将A自增了1


    }


    //class1类
    class Class1
    {
        public static int count = 0;
        static Class1()
        {
            count++;
        }
        public Class1()
        {
            count++;
        }
    }


}
