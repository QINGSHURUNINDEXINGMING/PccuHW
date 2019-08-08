namespace TestAll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.GOODs");
        }
    }
}
