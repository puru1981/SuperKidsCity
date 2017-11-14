namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Members", "Address_Id", "dbo.Addresses");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Schools", "Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Members", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Schools");
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
            AddForeignKey("dbo.Members", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
