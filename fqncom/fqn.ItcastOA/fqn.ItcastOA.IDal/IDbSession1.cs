 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.ItcastOA.IDal
{
	public partial interface IDbSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IBooksDal BooksDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IKeyWordsRankDal KeyWordsRankDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		ISearchDetailsDal SearchDetailsDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	}	
}