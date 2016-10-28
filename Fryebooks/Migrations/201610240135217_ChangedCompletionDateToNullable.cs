namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCompletionDateToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkRequests", "CompletionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkRequests", "CompletionDate", c => c.DateTime(nullable: false));
        }
    }
}
