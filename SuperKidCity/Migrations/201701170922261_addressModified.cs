namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Locality_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "Locality_Id");
            AddForeignKey("dbo.Addresses", "Locality_Id", "dbo.Localities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Locality_Id", "dbo.Localities");
            DropIndex("dbo.Addresses", new[] { "Locality_Id" });
            DropColumn("dbo.Addresses", "Locality_Id");
        }
    }
}
