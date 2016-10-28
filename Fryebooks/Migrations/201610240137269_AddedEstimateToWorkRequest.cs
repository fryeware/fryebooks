namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEstimateToWorkRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkRequests", "Estimate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkRequests", "Estimate");
        }
    }
}
