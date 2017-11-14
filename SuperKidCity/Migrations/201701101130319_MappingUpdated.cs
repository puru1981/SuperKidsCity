namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            AlterColumn("dbo.Contacts", "AddressId", c => c.Int());
            CreateIndex("dbo.Contacts", "AddressId");
            AddForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            AlterColumn("dbo.Contacts", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contacts", "AddressId");
            AddForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
