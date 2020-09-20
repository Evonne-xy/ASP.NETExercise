namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseTypeModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseTypes", "CourseTypeName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseTypes", "CourseTypeName", c => c.Int(nullable: false));
        }
    }
}
