using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.SqlServerDal
{
    public partial class S_ProvinceDal : IProvinceDal
    {
        private EF_Model db = CommonDal.CommonHelper.CheckDbModelAndGet();
        public int Add(S_Province t)
        {
            db.S_Province.Add(t);
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            //db.S_Province.Remove((from p in db.S_Province where p.ProvinceID == id select p).FirstOrDefault());

            //var province = (from p in db.S_Province where p.ProvinceID == id select p).FirstOrDefault();
            //db.Entry<S_Province>(province).State = System.Data.Entity.EntityState.Deleted;

            db.Entry<S_Province>(db.S_Province.FirstOrDefault(p => p.ProvinceID == id)).State = System.Data.Entity.EntityState.Deleted;

            return db.SaveChanges();
        }

        public int Update(S_Province t)
        {
            db.Entry<S_Province>(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        public S_Province Select(int id)
        {
            var provinceList = from p in db.S_Province
                               where p.ProvinceID == id
                               select p;
            return provinceList.FirstOrDefault();
        }

        public List<S_Province> SelectList(int rowSkip, int rowTake)
        {
            var provinceList = (from d in db.S_Province
                                select d).OrderBy(d => d.ProvinceID).Skip<S_Province>(rowSkip).Take(rowTake);
            return provinceList.ToList();
        }

        public int SelectCount()
        {
            return (from d in db.S_Province select d).Count();
        }
    }
}
