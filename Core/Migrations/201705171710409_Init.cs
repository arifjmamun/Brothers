namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.FileInfoes",
                c => new
                    {
                        FileInfoId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 255, unicode: false),
                        UploadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileInfoId)
                .ForeignKey("dbo.Uploads", t => t.UploadId, cascadeDelete: true)
                .Index(t => t.UploadId);
            
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        UploadId = c.Int(nullable: false, identity: true),
                        Drrive = c.String(nullable: false, maxLength: 5, unicode: false),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        UploadPath = c.String(),
                        Thumbnail = c.Byte(),
                        PublishDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UploadId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileInfoes", "UploadId", "dbo.Uploads");
            DropForeignKey("dbo.Uploads", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Uploads", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Uploads", new[] { "SubCategoryId" });
            DropIndex("dbo.Uploads", new[] { "CategoryId" });
            DropIndex("dbo.FileInfoes", new[] { "UploadId" });
            DropTable("dbo.SubCategories");
            DropTable("dbo.Uploads");
            DropTable("dbo.FileInfoes");
            DropTable("dbo.Categories");
        }
    }
}
