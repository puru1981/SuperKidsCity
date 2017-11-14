namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacilityGroupMemberControlValueUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacilityGroupMemberControlValues", "SchoolId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacilityGroupMemberControlValues", "SchoolId");
        }
    }
}
