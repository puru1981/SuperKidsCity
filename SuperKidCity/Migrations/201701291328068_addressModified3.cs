namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressModified3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Locality_Id", "dbo.Localities");
            DropIndex("dbo.Addresses", new[] { "Locality_Id" });
            AlterColumn("dbo.Addresses", "Locality_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Locality_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "Locality_Id");
            AddForeignKey("dbo.Addresses", "Locality_Id", "dbo.Localities", "Id");
        }
    }
}
