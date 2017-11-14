namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactAddressMappingUpdated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contacts", new[] { "Address_Id" });
            DropColumn("dbo.Contacts", "Id");
            RenameColumn(table: "dbo.Contacts", name: "Address_Id", newName: "Id");
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Contacts", "Id");
            CreateIndex("dbo.Contacts", "Id");
            CreateIndex("dbo.Contacts", "Address_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contacts", new[] { "Address_Id" });
            DropIndex("dbo.Contacts", new[] { "Id" });
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Int());
            AlterColumn("dbo.Contacts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contacts", "Id");
            RenameColumn(table: "dbo.Contacts", name: "Id", newName: "Address_Id");
            AddColumn("dbo.Contacts", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Contacts", "Address_Id");
        }
    }
}
