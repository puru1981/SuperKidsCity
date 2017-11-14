namespace SuperKidCity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentModifief : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Parents", name: "Detail_Id", newName: "Member_Id");
            RenameIndex(table: "dbo.Parents", name: "IX_Detail_Id", newName: "IX_Member_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Parents", name: "IX_Member_Id", newName: "IX_Detail_Id");
            RenameColumn(table: "dbo.Parents", name: "Member_Id", newName: "Detail_Id");
        }
    }
}
