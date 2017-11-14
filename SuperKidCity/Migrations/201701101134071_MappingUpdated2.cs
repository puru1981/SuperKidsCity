namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingUpdated2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Contacts", name: "AddressId", newName: "Address_Id");
            RenameIndex(table: "dbo.Contacts", name: "IX_AddressId", newName: "IX_Address_Id");
            AddColumn("dbo.Contacts", "School_Id", c => c.Int());
            CreateIndex("dbo.Contacts", "School_Id");
            AddForeignKey("dbo.Contacts", "School_Id", "dbo.Schools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "School_Id", "dbo.Schools");
            DropIndex("dbo.Contacts", new[] { "School_Id" });
            DropColumn("dbo.Contacts", "School_Id");
            RenameIndex(table: "dbo.Contacts", name: "IX_Address_Id", newName: "IX_AddressId");
            RenameColumn(table: "dbo.Contacts", name: "Address_Id", newName: "AddressId");
        }
    }
}
