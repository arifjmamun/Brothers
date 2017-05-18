namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uploads", "Drive", c => c.String(nullable: false, maxLength: 5, unicode: false));
            DropColumn("dbo.Uploads", "Drrive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uploads", "Drrive", c => c.String(nullable: false, maxLength: 5, unicode: false));
            DropColumn("dbo.Uploads", "Drive");
        }
    }
}
