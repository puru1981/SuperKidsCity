namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localityCityStateUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Localities", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "State_Id", "dbo.States");
            DropIndex("dbo.Cities", new[] { "State_Id" });
            DropIndex("dbo.Localities", new[] { "City_Id" });
            AlterColumn("dbo.Cities", "State_Id", c => c.Int());
            AlterColumn("dbo.Localities", "City_Id", c => c.Int());
            CreateIndex("dbo.Cities", "State_Id");
            CreateIndex("dbo.Localities", "City_Id");
            AddForeignKey("dbo.Localities", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Cities", "State_Id", "dbo.States", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "State_Id", "dbo.States");
            DropForeignKey("dbo.Localities", "City_Id", "dbo.Cities");
            DropIndex("dbo.Localities", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "State_Id" });
            AlterColumn("dbo.Localities", "City_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Cities", "State_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Localities", "City_Id");
            CreateIndex("dbo.Cities", "State_Id");
            AddForeignKey("dbo.Cities", "State_Id", "dbo.States", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Localities", "City_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
