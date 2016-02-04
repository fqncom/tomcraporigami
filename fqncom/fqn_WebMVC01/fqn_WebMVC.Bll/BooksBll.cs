using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.FactoryDal;
using fqn_WebMVC.IBll;
using fqn_WebMVC.IDal;

namespace fqn_WebMVC.Bll
{
    public partial class BooksBll : IBooksBll
    {
        private IBooksDal booksDal = AbstractFactory.CreateBooksInstance();

        public int Add(Model.Books obj)
        {
            return booksDal.Add(obj);
        }

        public int Delete(string whereStr)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.Books obj, string whereStr)
        {
            throw new NotImplementedException();
        }

        public Model.Books Select(string whereStr)
        {
            return booksDal.Select("");
        }
    }
}
