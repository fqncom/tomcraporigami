using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace fqn.MVC_EF.IDal
{
    //public abstract class AbstractDal : ICRUD_Dal<object>
    //{
    //    //想在此处使用抽象方法将对ef的验证直接放在这里进行，但是失败了
    //    public AbstractDal()
    //    {
    //        EF_Model db = null;
    //        if (HttpContext.Current.Items["EF_Model"] == null)
    //        {
    //            db = new EF_Model();
    //        }
    //        else
    //        {
    //            db = HttpContext.Current.Items["EF_Model"] as EF_Model;
    //        }
    //    }

    //    public int Add(object t)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Delete(string whereStr)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Update(object t, string whereStr)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public object select(string whereStr)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
