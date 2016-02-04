 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.Dal
{
		
	public partial class ActionInfoDal :BaseSqlServerDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class BooksDal :BaseSqlServerDal<Books>,IBooksDal
    {

    }
		
	public partial class DepartmentDal :BaseSqlServerDal<Department>,IDepartmentDal
    {

    }
		
	public partial class KeyWordsRankDal :BaseSqlServerDal<KeyWordsRank>,IKeyWordsRankDal
    {

    }
		
	public partial class R_UserInfo_ActionInfoDal :BaseSqlServerDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseSqlServerDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class SearchDetailsDal :BaseSqlServerDal<SearchDetails>,ISearchDetailsDal
    {

    }
		
	public partial class UserInfoDal :BaseSqlServerDal<UserInfo>,IUserInfoDal
    {

    }
	
}