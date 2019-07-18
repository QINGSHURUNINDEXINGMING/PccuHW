namespace all.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GOODS",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        discount = c.Int(nullable: false),
                        discount_price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UID);
            
            CreateTable(
                "dbo.HISTORies",
                c => new
                    {
                        user_ID = c.Int(nullable: false, identity: true),
                        good = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.user_ID);
            
            CreateTable(
                "dbo.QR_code",
                c => new
                    {
                        user_ID = c.Int(nullable: false, identity: true),
                        QRcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.user_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QR_code");
            DropTable("dbo.HISTORies");
            DropTable("dbo.GOODS");
        }
    }
}
