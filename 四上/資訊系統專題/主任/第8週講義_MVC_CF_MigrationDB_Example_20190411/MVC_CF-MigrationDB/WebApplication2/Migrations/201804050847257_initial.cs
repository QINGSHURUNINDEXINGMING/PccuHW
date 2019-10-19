namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testtbs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        price = c.Double(nullable: false),
                        type = c.String(nullable: false),
                        comment = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);         
        }
      
        public override void Down()
        {
            DropTable("dbo.Testtbs");
        }
    }
}
