using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.Model
{
    public partial class Users
    {
        private UserStates _userState = new UserStates();

        public UserStates UserState
        {
            get { return _userState; }
            set { _userState = value; }
        }
    }
}
