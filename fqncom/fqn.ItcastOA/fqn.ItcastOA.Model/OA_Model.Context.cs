﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace fqn.ItcastOA.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class fqn_OAEntities : DbContext
    {
        public fqn_OAEntities()
            : base("name=fqn_OAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<KeyWordsRank> KeyWordsRank { get; set; }
        public DbSet<SearchDetails> SearchDetails { get; set; }
    }
}
