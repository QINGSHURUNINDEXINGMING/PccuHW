namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
            CreateTable(
                "dbo.HISTORies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_ID = c.Int(nullable: false),
                        good = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.QR_CODE",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_ID = c.Int(nullable: false),
                        QRcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QR_CODE");
            DropTable("dbo.HISTORies");
            DropTable("dbo.GOODs");
        }
    }
}
