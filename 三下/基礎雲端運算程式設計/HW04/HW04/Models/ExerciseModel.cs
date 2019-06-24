namespace HW04.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExerciseModel : DbContext
    {
        public ExerciseModel()
            : base("name=ExerciseModel")
        {
        }

        public DbSet<Col> Cols { get; set; }
        public DbSet<Kind> Kinds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Col>().ToTable("ColsV2");
            modelBuilder.Entity<Kind>().ToTable("KindsV2");
        }
    }
}
