namespace FIT5032AssignmentV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProviderModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(),
                        ProviderAddress = c.String(),
                    })
                .PrimaryKey(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Providers");
        }
    }
}
