﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace giaothong.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class giaothongEntities : DbContext
    {
        public giaothongEntities()
            : base("name=giaothongEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GIAOVIEN> GIAOVIENs { get; set; }
        public virtual DbSet<GIAOVIEN_GCN> GIAOVIEN_GCN { get; set; }
        public virtual DbSet<province_city> province_city { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<USERS_ROLE> USERS_ROLE { get; set; }
        public virtual DbSet<XETAPLAI> XETAPLAIs { get; set; }
        public virtual DbSet<XETAPLAI_GP> XETAPLAI_GP { get; set; }
        public virtual DbSet<HangXe> HangXes { get; set; }
    }
}
