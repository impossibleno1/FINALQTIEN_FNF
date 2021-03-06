
//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.CashBillDetails = new HashSet<CashBillDetail>();
            this.InstallmentBillDetails = new HashSet<InstallmentBillDetail>();
        }
    
        public int ID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeID { get; set; }
        public int SalePrice { get; set; }
        public int OriginPrice { get; set; }
        public int InstallmentPrice { get; set; }
        public int Quantity { get; set; }
        public string Avatar { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<CashBillDetail> CashBillDetails { get; set; }
        public virtual ICollection<InstallmentBillDetail> InstallmentBillDetails { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
