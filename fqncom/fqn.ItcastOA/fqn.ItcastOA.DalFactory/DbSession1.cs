 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.Dal;
using fqn.ItcastOA.IDal;

namespace fqn.ItcastOA.DalFactory
{
	public partial class DbSession : IDbSession
    {
	

		private IActionInfoDal _actionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                return this._actionInfoDal ?? new ActionInfoDal();
            }
            set { this._actionInfoDal = value; }
        }
	

		private IBooksDal _booksDal;
        public IBooksDal BooksDal
        {
            get
            {
                return this._booksDal ?? new BooksDal();
            }
            set { this._booksDal = value; }
        }
	

		private IDepartmentDal _departmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                return this._departmentDal ?? new DepartmentDal();
            }
            set { this._departmentDal = value; }
        }
	

		private IKeyWordsRankDal _keyWordsRankDal;
        public IKeyWordsRankDal KeyWordsRankDal
        {
            get
            {
                return this._keyWordsRankDal ?? new KeyWordsRankDal();
            }
            set { this._keyWordsRankDal = value; }
        }
	

		private IR_UserInfo_ActionInfoDal _r_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                return this._r_UserInfo_ActionInfoDal ?? new R_UserInfo_ActionInfoDal();
            }
            set { this._r_UserInfo_ActionInfoDal = value; }
        }
	

		private IRoleInfoDal _roleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                return this._roleInfoDal ?? new RoleInfoDal();
            }
            set { this._roleInfoDal = value; }
        }
	

		private ISearchDetailsDal _searchDetailsDal;
        public ISearchDetailsDal SearchDetailsDal
        {
            get
            {
                return this._searchDetailsDal ?? new SearchDetailsDal();
            }
            set { this._searchDetailsDal = value; }
        }
	

		private IUserInfoDal _userInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                return this._userInfoDal ?? new UserInfoDal();
            }
            set { this._userInfoDal = value; }
        }
	}	
}