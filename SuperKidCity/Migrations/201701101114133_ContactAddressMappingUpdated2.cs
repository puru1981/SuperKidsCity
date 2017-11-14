namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactAddressMappingUpdated2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Contacts", name: "Address_Id", newName: "AddressId");
            RenameIndex(table: "dbo.Contacts", name: "IX_Address_Id", newName: "IX_AddressId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Contacts", name: "IX_AddressId", newName: "IX_Address_Id");
            RenameColumn(table: "dbo.Contacts", name: "AddressId", newName: "Address_Id");
        }
    }
}
