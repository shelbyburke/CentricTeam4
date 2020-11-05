using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentricTeam4.Models;
using System.Data.Entity;

namespace CentricTeam4.DAL
{
    public class MIS4200Context : DbContext
    {
        public MIS4200Context() : base("name=DefaultConnection")
        {

        }
        public DbSet<userData> userData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>(); }

        public System.Data.Entity.DbSet<CentricTeam4.Models.TestCoreValues> TestCoreValues { get; set; }
    }
}