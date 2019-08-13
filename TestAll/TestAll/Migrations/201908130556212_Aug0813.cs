namespace TestAll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aug0813 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.GOODs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GOODs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UID = c.String(nullable: false),
                        name = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        discount = c.Int(nullable: false),
                        discount_price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
