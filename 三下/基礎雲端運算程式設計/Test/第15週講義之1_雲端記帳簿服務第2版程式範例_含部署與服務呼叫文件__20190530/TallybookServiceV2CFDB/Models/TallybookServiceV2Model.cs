namespace TallybookServiceV2CFDB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TallybookServiceV2Model : DbContext
    {
        public TallybookServiceV2Model()
            : base("name=TallybookServiceV2Model")
        {
        }

        public DbSet<Tallybook> Tallybooks { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        // 將Entity<Friend>對應到資料表Ch2Friends，Entity<Employee>對應到資料表Ch2Employees
        // 若沒有以下設定，預設Entity<Friend>將對應到資料表Friends，Entity<Employee>對應到資料表Employees
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Tallybook>().ToTable("TallybooksV2CFDB");
            modelBuilder.Entity<ExpenseType>().ToTable("ExpenseTypesV2CFDB");
        }
    }
}
