namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DirectoryPathSetToNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Uploads", new[] { "DirectoryPath" });
            AlterColumn("dbo.Uploads", "DirectoryPath", c => c.String(maxLength: 255));
            CreateIndex("dbo.Uploads", "DirectoryPath", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Uploads", new[] { "DirectoryPath" });
            AlterColumn("dbo.Uploads", "DirectoryPath", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Uploads", "DirectoryPath", unique: true);
        }
    }
}
