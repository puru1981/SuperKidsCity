namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactAddressMappingUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "Address_Id" });
            AlterColumn("dbo.Contacts", "Address_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Contacts", "Address_Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "Address_Id" });
            AlterColumn("dbo.Contacts", "Address_Id", c => c.Int());
            CreateIndex("dbo.Contacts", "Address_Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
