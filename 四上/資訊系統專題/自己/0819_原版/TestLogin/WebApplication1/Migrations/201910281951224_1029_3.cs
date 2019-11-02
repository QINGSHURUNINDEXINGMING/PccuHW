namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1029_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HISTORies", "time", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HISTORies", "time");
        }
    }
}
