using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.IDal
{
    public interface ICRUD_Dal<T> where T : class
    {
        int Add(T t);

        int Delete(int id);

        int Update(T t);

        T Select(int id);

        List<T> SelectList(int rowSkip,int rowTake);

        int SelectCount();
    }
}
