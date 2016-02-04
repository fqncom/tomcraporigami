using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.IBll;
using fqn.MVC_EF.IDal;
using fqn.MVC_EF.FactoryDal;

namespace fqn.MVC_EF.Bll
{
    public class S_ProvinceBll : IProvinceBll
    {

        IProvinceDal dal = FactoryDal.AbstractFactory.CreateProvinceDalInstance();
        public bool Add(S_Province t)
        {
            return dal.Add(t) > 0;
        }

        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

        public bool Update(S_Province t)
        {
            return dal.Update(t) > 0;
        }

        public S_Province Select(int id)
        {
            return dal.Select(id);
        }

        public List<S_Province> SelectList(int rowSkip, int rowTake)
        {
            return dal.SelectList(rowSkip, rowTake);
        }


        public int SelectCount()
        {
            return dal.SelectCount();
        }
    }
}
