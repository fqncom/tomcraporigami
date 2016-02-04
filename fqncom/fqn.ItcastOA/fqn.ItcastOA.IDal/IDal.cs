 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;

namespace fqn.ItcastOA.IDal
{
   
	
	public partial interface IActionInfoDal :IBaseDal<ActionInfo>
    {
      
    }
	
	public partial interface IBooksDal :IBaseDal<Books>
    {
      
    }
	
	public partial interface IDepartmentDal :IBaseDal<Department>
    {
      
    }
	
	public partial interface IKeyWordsRankDal :IBaseDal<KeyWordsRank>
    {
      
    }
	
	public partial interface IR_UserInfo_ActionInfoDal :IBaseDal<R_UserInfo_ActionInfo>
    {
      
    }
	
	public partial interface IRoleInfoDal :IBaseDal<RoleInfo>
    {
      
    }
	
	public partial interface ISearchDetailsDal :IBaseDal<SearchDetails>
    {
      
    }
	
	public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
      
    }
	
}