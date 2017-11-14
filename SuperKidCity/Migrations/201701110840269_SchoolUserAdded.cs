namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolUserAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SessionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .Index(t => t.School_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolUsers", "School_Id", "dbo.Schools");
            DropIndex("dbo.SchoolUsers", new[] { "School_Id" });
            DropTable("dbo.SchoolUsers");
        }
    }
}
