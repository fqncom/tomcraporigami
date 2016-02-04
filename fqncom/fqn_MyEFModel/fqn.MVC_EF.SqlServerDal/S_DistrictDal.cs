using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.SqlServerDal
{
    public class S_DistrictDal : IDistrictDal
    {
        EF_Model db = CommonDal.CommonHelper.CheckDbModelAndGet();
        public int Add(S_District t)
        {
            db.S_District.Add(t);
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(S_District t)
        {
            throw new NotImplementedException();
        }

        public S_District Select(int id)
        {
            var districtList = from d in db.S_District
                               where d.DistrictID == id
                               select d;
            return districtList.FirstOrDefault();
        }

        public List<S_District> SelectList(int rowSkip, int rowTake)
        {
            var districtList = (from d in db.S_District
                                orderby d.CityID descending 
                                select d).Skip<S_District>(rowSkip).Take(rowTake);
            return districtList.ToList();
        }

        public int SelectCount()
        {
            return (from d in db.S_District select d).Count();
        }
    }
}
