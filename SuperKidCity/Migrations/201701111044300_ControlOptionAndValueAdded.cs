namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlOptionAndValueAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacilityGroupMemberControlOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Option = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        GroupMember_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityGroupMembers", t => t.GroupMember_Id)
                .Index(t => t.GroupMember_Id);
            
            CreateTable(
                "dbo.FacilityGroupMemberControlValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        DataType = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Control_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityGroupMembers", t => t.Control_Id)
                .Index(t => t.Control_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacilityGroupMemberControlValues", "Control_Id", "dbo.FacilityGroupMembers");
            DropForeignKey("dbo.FacilityGroupMemberControlOptions", "GroupMember_Id", "dbo.FacilityGroupMembers");
            DropIndex("dbo.FacilityGroupMemberControlValues", new[] { "Control_Id" });
            DropIndex("dbo.FacilityGroupMemberControlOptions", new[] { "GroupMember_Id" });
            DropTable("dbo.FacilityGroupMemberControlValues");
            DropTable("dbo.FacilityGroupMemberControlOptions");
        }
    }
}
