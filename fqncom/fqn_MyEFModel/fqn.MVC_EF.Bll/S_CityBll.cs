using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.FactoryDal;
using fqn.MVC_EF.IBll;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.Bll
{
    public class S_CityBll : ICityBll
    {
        ICityDal dal = AbstractFactory.CreateCityDalInstance();
        public bool Add(S_City t)
        {
            return dal.Add(t) > 0;
        }

        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

        public bool Update(S_City t)
        {
            return dal.Update(t) > 0;
        }

        public S_City Select(int id)
        {
            return dal.Select(id);
        }

        public List<S_City> SelectList(int rowSkip, int rowTake)
        {
            return dal.SelectList(rowSkip, rowTake);
        }

        public int SelectCount()
        {
            return dal.SelectCount();
        }
    }
}
