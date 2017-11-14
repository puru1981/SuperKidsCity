namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localityEntityModified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Localities", "City_Id", "dbo.Cities");
            DropIndex("dbo.Localities", new[] { "City_Id" });
            RenameColumn(table: "dbo.Localities", name: "City_Id", newName: "CityId");
            AlterColumn("dbo.Localities", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Localities", "CityId");
            AddForeignKey("dbo.Localities", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Localities", "CityId", "dbo.Cities");
            DropIndex("dbo.Localities", new[] { "CityId" });
            AlterColumn("dbo.Localities", "CityId", c => c.Int());
            RenameColumn(table: "dbo.Localities", name: "CityId", newName: "City_Id");
            CreateIndex("dbo.Localities", "City_Id");
            AddForeignKey("dbo.Localities", "City_Id", "dbo.Cities", "Id");
        }
    }
}
