
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DIENMAYQUYETTIEN2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DIENMAYQUYETTIENEntities : DbContext
    {
        public DIENMAYQUYETTIENEntities()
            : base("name=DIENMAYQUYETTIENEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CashBill> CashBills { get; set; }
        public virtual DbSet<CashBillDetail> CashBillDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InstallmentBill> InstallmentBills { get; set; }
        public virtual DbSet<InstallmentBillDetail> InstallmentBillDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
    }
}
