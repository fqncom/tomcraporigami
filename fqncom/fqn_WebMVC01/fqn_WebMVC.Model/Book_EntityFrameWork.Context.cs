﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace fqn_WebMVC.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Book_ShopEntities : DbContext
    {
        public Book_ShopEntities()
            : base("name=Book_ShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Articel_Words> Articel_Words { get; set; }
        public DbSet<BookComment> BookComment { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CheckEmail> CheckEmail { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<SysFun> SysFun { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserStates> UserStates { get; set; }
        public DbSet<VidoFile> VidoFile { get; set; }
    
        public virtual int create_OrderConfrim(string orderId, Nullable<int> userId, string address, ObjectParameter totalmoney)
        {
            var orderIdParameter = orderId != null ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("create_OrderConfrim", orderIdParameter, userIdParameter, addressParameter, totalmoney);
        }
    
        public virtual int createOrderConfirm(string ordernumber, string address, Nullable<int> userId, ObjectParameter totalMoney)
        {
            var ordernumberParameter = ordernumber != null ?
                new ObjectParameter("ordernumber", ordernumber) :
                new ObjectParameter("ordernumber", typeof(string));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("createOrderConfirm", ordernumberParameter, addressParameter, userIdParameter, totalMoney);
        }
    
        public virtual int Pro_CreateOrder(Nullable<int> userId, string orderNumber, string postAddress, ObjectParameter totalPrice)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var orderNumberParameter = orderNumber != null ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(string));
    
            var postAddressParameter = postAddress != null ?
                new ObjectParameter("PostAddress", postAddress) :
                new ObjectParameter("PostAddress", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_CreateOrder", userIdParameter, orderNumberParameter, postAddressParameter, totalPrice);
        }
    
        public virtual int Pro_createOrdersConfirm(string ordernum, string address, Nullable<int> userid, ObjectParameter totalMoney)
        {
            var ordernumParameter = ordernum != null ?
                new ObjectParameter("ordernum", ordernum) :
                new ObjectParameter("ordernum", typeof(string));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_createOrdersConfirm", ordernumParameter, addressParameter, useridParameter, totalMoney);
        }
    
        public virtual int Pro_GetPagedList(Nullable<int> start, Nullable<int> end, Nullable<int> category, string order)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(int));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(int));
    
            var categoryParameter = category.HasValue ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(int));
    
            var orderParameter = order != null ?
                new ObjectParameter("order", order) :
                new ObjectParameter("order", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_GetPagedList", startParameter, endParameter, categoryParameter, orderParameter);
        }
    
        public virtual int Pro_OrderCreate(string orderNmber, Nullable<int> userId, string address, ObjectParameter totalMoney)
        {
            var orderNmberParameter = orderNmber != null ?
                new ObjectParameter("OrderNmber", orderNmber) :
                new ObjectParameter("OrderNmber", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_OrderCreate", orderNmberParameter, userIdParameter, addressParameter, totalMoney);
        }
    
        public virtual int Pro_Page(string tblName, string strGetFields, string fldName, Nullable<int> pageSize, Nullable<int> pageIndex, Nullable<bool> doCount, Nullable<bool> orderType, string strWhere)
        {
            var tblNameParameter = tblName != null ?
                new ObjectParameter("tblName", tblName) :
                new ObjectParameter("tblName", typeof(string));
    
            var strGetFieldsParameter = strGetFields != null ?
                new ObjectParameter("strGetFields", strGetFields) :
                new ObjectParameter("strGetFields", typeof(string));
    
            var fldNameParameter = fldName != null ?
                new ObjectParameter("fldName", fldName) :
                new ObjectParameter("fldName", typeof(string));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var doCountParameter = doCount.HasValue ?
                new ObjectParameter("doCount", doCount) :
                new ObjectParameter("doCount", typeof(bool));
    
            var orderTypeParameter = orderType.HasValue ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(bool));
    
            var strWhereParameter = strWhere != null ?
                new ObjectParameter("strWhere", strWhere) :
                new ObjectParameter("strWhere", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_Page", tblNameParameter, strGetFieldsParameter, fldNameParameter, pageSizeParameter, pageIndexParameter, doCountParameter, orderTypeParameter, strWhereParameter);
        }
    
        public virtual int Proc_OrderConfirm(Nullable<int> orderId, Nullable<int> userId, string address, ObjectParameter totalMoney)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Proc_OrderConfirm", orderIdParameter, userIdParameter, addressParameter, totalMoney);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int usp_createOrderConfirm(string ordernumber, Nullable<int> userId, string address, ObjectParameter totalMoney)
        {
            var ordernumberParameter = ordernumber != null ?
                new ObjectParameter("ordernumber", ordernumber) :
                new ObjectParameter("ordernumber", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_createOrderConfirm", ordernumberParameter, userIdParameter, addressParameter, totalMoney);
        }
    }
}