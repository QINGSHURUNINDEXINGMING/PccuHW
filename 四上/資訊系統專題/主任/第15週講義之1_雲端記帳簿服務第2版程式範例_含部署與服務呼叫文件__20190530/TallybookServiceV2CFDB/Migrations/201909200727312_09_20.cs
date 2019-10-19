namespace TallybookServiceV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09_20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseTypesV2CFDB",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expenseType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TallybooksV2CFDB",
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
            DropTable("dbo.TallybooksV2CFDB");
            DropTable("dbo.ExpenseTypesV2CFDB");
        }
    }
}
