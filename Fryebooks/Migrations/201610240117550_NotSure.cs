namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotSure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContactName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Url = c.String(),
                        Email = c.String(),
                        OnBoardDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.WorkRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.WorkResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStarted = c.DateTime(nullable: false),
                        TimeWorked = c.Time(nullable: false, precision: 7),
                        Description = c.String(),
                        WorkRequestId = c.Int(nullable: false),
                        Billable = c.Boolean(nullable: false),
                        IncomeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incomes", t => t.IncomeId, cascadeDelete: true)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequestId, cascadeDelete: true)
                .Index(t => t.WorkRequestId)
                .Index(t => t.IncomeId);
            
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayDay = c.DateTime(nullable: false),
                        GrossPayment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkResponses", "WorkRequestId", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes");
            DropForeignKey("dbo.WorkRequests", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Alerts", "ClientId", "dbo.Clients");
            DropIndex("dbo.WorkResponses", new[] { "IncomeId" });
            DropIndex("dbo.WorkResponses", new[] { "WorkRequestId" });
            DropIndex("dbo.WorkRequests", new[] { "ClientId" });
            DropIndex("dbo.Alerts", new[] { "ClientId" });
            DropTable("dbo.Incomes");
            DropTable("dbo.WorkResponses");
            DropTable("dbo.WorkRequests");
            DropTable("dbo.Alerts");
            DropTable("dbo.Clients");
        }
    }
}
