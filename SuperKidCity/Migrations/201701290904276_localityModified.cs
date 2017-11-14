namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localityModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Localities", "District", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Localities", "District");
        }
    }
}
