namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingUpdated3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacilityGroupMembers", "ValueType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacilityGroupMembers", "ValueType");
        }
    }
}
