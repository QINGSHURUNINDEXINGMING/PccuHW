namespace HW04.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ServiceModel1 : DbContext
    {
        public ServiceModel1()
            : base("name=ServiceModel1")
        {
        }

        public DbSet<Col> Col { get; set; }
        public DbSet<Type> Type { get; set; }

        // 將Entity<Friend>對應到資料表Ch2Friends，Entity<Employee>對應到資料表Ch2Employees
        // 若沒有以下設定，預設Entity<Friend>將對應到資料表Friends，Entity<Employee>對應到資料表Employees


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Col>().ToTable("Col");
            modelBuilder.Entity<Type>().ToTable("Type");
        }
    }
}
