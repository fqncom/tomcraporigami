using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.Model
{
    public partial class Cart
    {
       
        #region Model
        
        private Users _user = new Users();

        private Books _book = new Books();


        /// <summary>
        /// 
        /// </summary>

        public Users User
        {
            get { return _user; }
            set { _user = value; }
        }
        public Books Book
        {
            get { return _book; }
            set { _book = value; }
        }
        
        #endregion Model
    }
}
