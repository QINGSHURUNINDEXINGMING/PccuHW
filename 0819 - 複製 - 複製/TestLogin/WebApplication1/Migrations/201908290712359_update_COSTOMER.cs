namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_COSTOMER : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COSTOMERs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        UID = c.String(nullable: false),
                        wallet = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.COSTOMERs");
        }
    }
}
