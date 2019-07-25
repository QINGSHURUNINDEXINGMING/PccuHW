namespace Test.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ConnectDB : DbContext
    {
        public ConnectDB()
            : base("name=ConnectDB")
        {
        }

        public DbSet<QR_code> QRcode1 { get; set; }
        public DbSet<GOOD> Good1 { get; set; }
        public DbSet<HISTORY> History1 { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Test.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}
