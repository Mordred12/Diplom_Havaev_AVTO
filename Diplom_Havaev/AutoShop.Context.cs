﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diplom_Havaev
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AutoShop_BDEntities : DbContext
    {
        public static AutoShop_BDEntities context;
        public AutoShop_BDEntities()
            : base("name=AutoShop_BDEntities")
        {
        }

        public static AutoShop_BDEntities GetContext()
        {
            if (context == null)
                context = new AutoShop_BDEntities();
            return context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProduceForSale> ProduceForSale { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
