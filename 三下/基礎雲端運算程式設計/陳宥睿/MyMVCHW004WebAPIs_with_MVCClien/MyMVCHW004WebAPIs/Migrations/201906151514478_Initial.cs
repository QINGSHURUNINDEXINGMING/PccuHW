namespace MyMVCHW004WebAPIs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HW004Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        location = c.String(),
                        sportTime = c.Int(nullable: false),
                        sportType = c.String(),
                        sportDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.HW004SportTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sportType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HW004SportTypes");
            DropTable("dbo.HW004Products");
        }
    }
}
