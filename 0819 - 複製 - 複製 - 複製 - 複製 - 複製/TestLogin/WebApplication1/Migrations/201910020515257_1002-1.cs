namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10021 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.COSTOMERs");
        }
        
        public override void Down()
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
    }
}
