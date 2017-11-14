namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parents",
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Detail_Id = c.Int(nullable: false),
                        School_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Detail_Id, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.School_Id, cascadeDelete: true)
                .Index(t => t.Detail_Id)
                .Index(t => t.School_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parents", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Parents", "Detail_Id", "dbo.Members");
            DropIndex("dbo.Parents", new[] { "School_Id" });
            DropIndex("dbo.Parents", new[] { "Detail_Id" });
            DropTable("dbo.Parents");
        }
    }
}
