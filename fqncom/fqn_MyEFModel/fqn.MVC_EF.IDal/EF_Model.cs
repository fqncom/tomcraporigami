using System.Data.Entity.ModelConfiguration.Conventions;

namespace fqn.MVC_EF.IDal
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EF_Model : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“EF_Model”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“fqn.MVC_EF.IDal.EF_Model”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“EF_Model”
        //连接字符串。
        public EF_Model()
            : base("name=EF_Model")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //在将实体映射成表时，去掉复数形式。
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<S_Province> S_Province { get; set; }
        public virtual DbSet<S_City> S_City { get; set; }
        public virtual DbSet<S_District> S_District { get; set; }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}