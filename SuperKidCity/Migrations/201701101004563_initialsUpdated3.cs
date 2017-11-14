namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsUpdated3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.Schools");
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Members", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AddColumn("dbo.Addresses", "School_Id", c => c.Int());
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "School_Id");
            AddForeignKey("dbo.Addresses", "School_Id", "dbo.Schools", "Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Members", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "School_Id", "dbo.Schools");
            DropIndex("dbo.Addresses", new[] { "School_Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "School_Id");
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Members", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Schools", "Id");
        }
    }
}
