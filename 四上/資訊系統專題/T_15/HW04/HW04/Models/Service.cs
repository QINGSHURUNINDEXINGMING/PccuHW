namespace HW04.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Service : DbContext
    {
        public Service()
            : base("name=Service")
        {
        }

        public DbSet<MainTB> MainTBs { get; set; }
        public DbSet<AddKind> AddKinds { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainTB>().ToTable("MainTBV2");
            modelBuilder.Entity<AddKind>().ToTable("AddKindV2");
        }
    }
}
