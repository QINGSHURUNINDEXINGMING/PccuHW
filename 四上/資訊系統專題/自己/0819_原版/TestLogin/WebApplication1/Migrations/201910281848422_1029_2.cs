namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1029_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HISTORies", "GName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HISTORies", "GName");
        }
    }
}
