using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 匿名类型
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 匿名类型,在反编译之后看到的生成了一个特殊的类。

            var car = new { Brand = "宝马", PaiLiang = "6.0", Ower = "我" };

            //匿名类型不能被赋值，是只读的，
            //car.Brand = "奔驰";

            Console.WriteLine(car.Brand);
            #endregion

            Console.ReadKey();
        }
    }
}
