namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFryebooksEntityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpenseAmount = c.Double(nullable: false),
                        Refundable = c.Boolean(nullable: false),
                        ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.WorkRespons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStarted = c.DateTime(nullable: false),
                        TimeWorked = c.Time(nullable: false, precision: 7),
                        Description = c.String(),
                        WorkRequestId = c.Int(nullable: false),
                        Billable = c.Boolean(nullable: false),
                        IncomeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.WorkRequestId)
                .Index(t => t.IncomeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expens", "ClientId", "dbo.Clients");
            DropIndex("dbo.WorkRespons", new[] { "IncomeId" });
            DropIndex("dbo.WorkRespons", new[] { "WorkRequestId" });
            DropIndex("dbo.Expens", new[] { "ClientId" });
            DropTable("dbo.WorkRespons");
            DropTable("dbo.Expens");
        }
    }
}
