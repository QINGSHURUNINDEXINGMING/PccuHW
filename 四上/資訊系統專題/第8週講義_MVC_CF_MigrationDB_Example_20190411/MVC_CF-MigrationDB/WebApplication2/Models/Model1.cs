namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {

        public Model1()
            : base("name=Model1")
        {
        }

        public DbSet<Testtb> Testtables { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
