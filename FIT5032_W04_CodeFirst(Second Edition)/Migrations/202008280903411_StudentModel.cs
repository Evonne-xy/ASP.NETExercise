namespace FIT5032_W04_CodeFirst_Second_Edition_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UnitID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "StudentID", "dbo.Students");
            DropIndex("dbo.Units", new[] { "StudentID" });
            DropTable("dbo.Units");
            DropTable("dbo.Students");
        }
    }
}
