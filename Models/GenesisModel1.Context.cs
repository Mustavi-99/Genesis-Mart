﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Genesis_Mart.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GenesisMartEntities : DbContext
    {
        public GenesisMartEntities()
            : base("name=GenesisMartEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
    }
}
