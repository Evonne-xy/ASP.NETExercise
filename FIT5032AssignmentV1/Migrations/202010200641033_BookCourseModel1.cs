namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookCourseModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCourses", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookCourses", "Rating");
        }
    }
}
