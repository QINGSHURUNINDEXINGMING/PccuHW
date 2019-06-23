namespace HW04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Col",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        type = c.String(),
                        comment = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        extype = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Type");
            DropTable("dbo.Col");
        }
    }
}
