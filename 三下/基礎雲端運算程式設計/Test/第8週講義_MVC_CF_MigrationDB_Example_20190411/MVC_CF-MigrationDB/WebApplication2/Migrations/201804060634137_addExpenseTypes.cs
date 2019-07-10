namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExpenseTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expenseType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpenseTypes");
        }
    }
}
