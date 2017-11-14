namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        Locality_Id = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .Index(t => t.School_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Email = c.String(),
                        Type = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        School_Id = c.Int(),
                        AddressDTO_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AddressDTOes", t => t.AddressDTO_Id)
                .Index(t => t.School_Id)
                .Index(t => t.AddressDTO_Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegistrationNumber = c.String(),
                        EstablishedAt = c.DateTime(nullable: false),
                        WebUrl = c.String(),
                        ShortBio = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirtsName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        School_Id = c.Int(),
                        AddressDTO_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AddressDTOes", t => t.AddressDTO_Id)
                .Index(t => t.School_Id)
                .Index(t => t.AddressDTO_Id);

            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        StateId = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        StateDTO_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateDTOes", t => t.StateDTO_Id)
                .Index(t => t.StateDTO_Id);

            CreateTable(
                "dbo.Localities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        District = c.String(),
                        CityId = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);

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
                "dbo.FacilityGroupMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ValueType = c.String(),
                        GUID = c.Guid(nullable: false),
                        Required = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityGroups", t => t.Group_Id)
                .Index(t => t.Group_Id);

            CreateTable(
                "dbo.FacilityGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.FacilityGroupMemberControlValueDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        DataType = c.String(),
                        SchoolId = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Control_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityGroupMembers", t => t.Control_Id)
                .Index(t => t.Control_Id);

            CreateTable(
                "dbo.BaseLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IP = c.String(),
                        Platform = c.String(),
                        Message = c.String(),
                        HasError = c.Boolean(nullable: false),
                        Entity = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParentDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentRegdNo = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Member_Id = c.Int(),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .Index(t => t.Member_Id)
                .Index(t => t.School_Id);
            
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SchoolUserDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .Index(t => t.School_Id);

            CreateTable(
                "dbo.StateDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cities", "StateDTO_Id", "dbo.StateDTOes");
            DropForeignKey("dbo.Localities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.SchoolUserDTOes", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ParentDTOes", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.ParentDTOes", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.FacilityGroupMemberControlValueDTOes", "Control_Id", "dbo.FacilityGroupMembers");
            DropForeignKey("dbo.FacilityGroupMemberControlOptions", "GroupMember_Id", "dbo.FacilityGroupMembers");
            DropForeignKey("dbo.FacilityGroupMembers", "Group_Id", "dbo.FacilityGroups");
            DropForeignKey("dbo.AddressDTOes", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Members", "AddressDTO_Id", "dbo.AddressDTOes");
            DropForeignKey("dbo.Members", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Contacts", "AddressDTO_Id", "dbo.AddressDTOes");
            DropForeignKey("dbo.Contacts", "School_Id", "dbo.Schools");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SchoolUserDTOes", new[] { "School_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ParentDTOes", new[] { "School_Id" });
            DropIndex("dbo.ParentDTOes", new[] { "Member_Id" });
            DropIndex("dbo.FacilityGroupMemberControlValueDTOes", new[] { "Control_Id" });
            DropIndex("dbo.FacilityGroupMembers", new[] { "Group_Id" });
            DropIndex("dbo.FacilityGroupMemberControlOptions", new[] { "GroupMember_Id" });
            DropIndex("dbo.Localities", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "StateDTO_Id" });
            DropIndex("dbo.Members", new[] { "AddressDTO_Id" });
            DropIndex("dbo.Members", new[] { "School_Id" });
            DropIndex("dbo.Contacts", new[] { "AddressDTO_Id" });
            DropIndex("dbo.Contacts", new[] { "School_Id" });
            DropIndex("dbo.AddressDTOes", new[] { "School_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StateDTOes");
            DropTable("dbo.SchoolUserDTOes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ParentDTOes");
            DropTable("dbo.BaseLogs");
            DropTable("dbo.FacilityGroupMemberControlValueDTOes");
            DropTable("dbo.FacilityGroups");
            DropTable("dbo.FacilityGroupMembers");
            DropTable("dbo.FacilityGroupMemberControlOptions");
            DropTable("dbo.Localities");
            DropTable("dbo.Cities");
            DropTable("dbo.Members");
            DropTable("dbo.Schools");
            DropTable("dbo.Contacts");
            DropTable("dbo.AddressDTOes");
        }
    }
}
