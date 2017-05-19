using System.Data;

namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadModelChangeMadeUniqueColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uploads", "DirectoryPath", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Uploads", "DirectoryPath", unique: true);
            DropColumn("dbo.Uploads", "UploadPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uploads", "UploadPath", c => c.String());
            DropIndex("dbo.Uploads", new[] { "DirectoryPath" });
            DropColumn("dbo.Uploads", "DirectoryPath");
        }
    }
}
