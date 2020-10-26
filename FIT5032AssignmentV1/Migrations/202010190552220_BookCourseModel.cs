namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookCourseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCourses",
                c => new
                    {
                        BookCourseId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        ProviderCourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookCourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.ProviderCourses", t => t.ProviderCourseId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProviderCourseId);
            
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.BookCourses", "ProviderCourseId", "dbo.ProviderCourses");
            DropForeignKey("dbo.BookCourses", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookCourses", new[] { "ProviderCourseId" });
            DropIndex("dbo.BookCourses", new[] { "ApplicationUserId" });
            DropTable("dbo.BookCourses");
            
        }
    }
}
