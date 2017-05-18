namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadModelChangedToAcceptFile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uploads", "Thumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uploads", "Thumbnail", c => c.Byte());
        }
    }
}
