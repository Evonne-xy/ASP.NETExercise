namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProviderCourseModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProviderCourses", "AggregateRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProviderCourses", "AggregateRating");
        }
    }
}
