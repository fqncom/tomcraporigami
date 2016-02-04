 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.DalFactory;
using fqn.ItcastOA.IBll;
using fqn.ItcastOA.IDal;
using fqn.ItcastOA.Model;
using fqn.ItcastOA.Model.Enum;
using fqn.ItcastOA.Model.SearchModel;

namespace fqn.ItcastOA.Bll
{
	
	public partial class ActionInfoBll :BaseBll<ActionInfo>,IActionInfoBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.ActionInfoDal;
        }
    }   
	
	public partial class BooksBll :BaseBll<Books>,IBooksBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.BooksDal;
        }
    }   
	
	public partial class DepartmentBll :BaseBll<Department>,IDepartmentBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.DepartmentDal;
        }
    }   
	
	public partial class KeyWordsRankBll :BaseBll<KeyWordsRank>,IKeyWordsRankBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.KeyWordsRankDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoBll :BaseBll<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoBll :BaseBll<RoleInfo>,IRoleInfoBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.RoleInfoDal;
        }
    }   
	
	public partial class SearchDetailsBll :BaseBll<SearchDetails>,ISearchDetailsBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.SearchDetailsDal;
        }
    }   
	
	public partial class UserInfoBll :BaseBll<UserInfo>,IUserInfoBll
    {
        public override void SetCurrentDal()
        {
			base.CurrentDal = base.GetDbSession.UserInfoDal;
        }
    }   
	
}