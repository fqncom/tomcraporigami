using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.SqlServerDal
{
    public class S_CityDal : ICityDal
    {

        EF_Model db = CommonDal.CommonHelper.CheckDbModelAndGet();
        public int Add(S_City t)
        {
            db.S_City.Add(t);
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(S_City t)
        {
            throw new NotImplementedException();
        }

        public S_City Select(int id)
        {
            var cityList = from c in db.S_City
                           where c.CityID == id
                           select c;
            return cityList.FirstOrDefault();
        }

        public List<S_City> SelectList(int rowSkip, int rowTake)
        {
            var cityList = (from d in db.S_City
                            orderby d.CityID ascending
                            select d).Skip<S_City>(rowSkip).Take(rowTake);
            return cityList.ToList();
        }

        public int SelectCount()
        {
            return (from d in db.S_City select d).Count();
        }
    }
}
