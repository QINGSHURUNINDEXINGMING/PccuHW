namespace HW04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddKinds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MainTBs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        type = c.String(),
                        comment = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MainTBs");
            DropTable("dbo.AddKinds");
        }
    }
}
