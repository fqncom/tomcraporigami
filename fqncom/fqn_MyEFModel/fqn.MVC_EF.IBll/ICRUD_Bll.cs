using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.IBll
{
    public interface ICRUD_Bll<T> where T : class ,new()
    {
        bool Add(T t);

        bool Delete(int id);

        bool Update(T t);

        T Select(int id);

        List<T> SelectList(int rowSkip, int rowTake);

        int SelectCount();

    }
}
