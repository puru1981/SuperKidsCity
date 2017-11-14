namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initials3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FacilityGroupMembers", name: "FacilityGroup_Id", newName: "Group_Id");
            RenameIndex(table: "dbo.FacilityGroupMembers", name: "IX_FacilityGroup_Id", newName: "IX_Group_Id");
            DropColumn("dbo.FacilityGroupMembers", "GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FacilityGroupMembers", "GroupId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.FacilityGroupMembers", name: "IX_Group_Id", newName: "IX_FacilityGroup_Id");
            RenameColumn(table: "dbo.FacilityGroupMembers", name: "Group_Id", newName: "FacilityGroup_Id");
        }
    }
}
