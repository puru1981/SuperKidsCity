namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COntactModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Type");
        }
    }
}
