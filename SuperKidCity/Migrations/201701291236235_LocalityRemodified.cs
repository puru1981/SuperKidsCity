namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalityRemodified : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Addresses", new[] { "Locality_Id" });
            DropColumn("dbo.Localities", "Id");
            RenameColumn(table: "dbo.Localities", name: "Locality_Id", newName: "Id");
            DropPrimaryKey("dbo.Localities");
            AlterColumn("dbo.Localities", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Localities", "Id");
            CreateIndex("dbo.Localities", "Id");
            DropColumn("dbo.Addresses", "Locality_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Locality_Id", c => c.Int());
            DropIndex("dbo.Localities", new[] { "Id" });
            DropPrimaryKey("dbo.Localities");
            AlterColumn("dbo.Localities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Localities", "Id");
            RenameColumn(table: "dbo.Localities", name: "Id", newName: "Locality_Id");
            AddColumn("dbo.Localities", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Addresses", "Locality_Id");
        }
    }
}
