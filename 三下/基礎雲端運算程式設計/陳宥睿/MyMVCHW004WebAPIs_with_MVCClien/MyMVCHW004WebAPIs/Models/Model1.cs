namespace MyMVCHW004WebAPIs.Models
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

		public DbSet<Sport> Sports { get; set; }
		public DbSet<SportType> SportTypes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sport>().ToTable("HW004Sports");
			modelBuilder.Entity<SportType>().ToTable("HW004SportTypes");
		}
	}
}