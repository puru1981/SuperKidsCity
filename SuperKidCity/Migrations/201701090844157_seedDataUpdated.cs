namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedDataUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Person_Id", "dbo.Members");
            DropIndex("dbo.Addresses", new[] { "Person_Id" });
            CreateTable(
                "dbo.FacilityGroupMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GUID = c.Guid(nullable: false),
                        Required = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        FacilityGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityGroups", t => t.FacilityGroup_Id)
                .Index(t => t.FacilityGroup_Id);
            
            CreateTable(
                "dbo.FacilityGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Members", "Address_Id", c => c.Int());
            CreateIndex("dbo.Members", "Address_Id");
            AddForeignKey("dbo.Members", "Address_Id", "dbo.Addresses", "Id");
            DropColumn("dbo.Addresses", "Person_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Person_Id", c => c.Int());
            DropForeignKey("dbo.FacilityGroupMembers", "FacilityGroup_Id", "dbo.FacilityGroups");
            DropForeignKey("dbo.Members", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.FacilityGroupMembers", new[] { "FacilityGroup_Id" });
            DropIndex("dbo.Members", new[] { "Address_Id" });
            DropColumn("dbo.Members", "Address_Id");
            DropTable("dbo.FacilityGroups");
            DropTable("dbo.FacilityGroupMembers");
            CreateIndex("dbo.Addresses", "Person_Id");
            AddForeignKey("dbo.Addresses", "Person_Id", "dbo.Members", "Id");
        }
    }
}
