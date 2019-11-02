namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1029_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HISTORies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        GUID = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HISTORies");
        }
    }
}
