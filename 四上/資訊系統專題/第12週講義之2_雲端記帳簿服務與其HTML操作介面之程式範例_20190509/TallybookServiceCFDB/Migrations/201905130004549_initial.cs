namespace TallybookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseTypesCFDB",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expenseType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TallybooksCFDB",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        price = c.Int(nullable: false),
                        expenseType = c.String(),
                        comment = c.String(),
                        payDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TallybooksCFDB");
            DropTable("dbo.ExpenseTypesCFDB");
        }
    }
}
