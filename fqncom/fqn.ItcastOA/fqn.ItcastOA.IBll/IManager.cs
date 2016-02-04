 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.SearchModel;

namespace fqn.ItcastOA.IBll
{
	
	public partial interface IActionInfoBll : IBaseBll<ActionInfo>
    {
       
    }   
	
	public partial interface IBooksBll : IBaseBll<Books>
    {
       
    }   
	
	public partial interface IDepartmentBll : IBaseBll<Department>
    {
       
    }   
	
	public partial interface IKeyWordsRankBll : IBaseBll<KeyWordsRank>
	{
	    
	}   
	
	public partial interface IR_UserInfo_ActionInfoBll : IBaseBll<R_UserInfo_ActionInfo>
    {
       
    }   
	
	public partial interface IRoleInfoBll : IBaseBll<RoleInfo>
    {
       
    }   
	
	public partial interface ISearchDetailsBll : IBaseBll<SearchDetails>
    {
       
    }   
	
	public partial interface IUserInfoBll : IBaseBll<UserInfo>
    {
       
    }   
	
}