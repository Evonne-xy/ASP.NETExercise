namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileUploadModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.fileUploads",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.fileUploads");
        }
    }
}
