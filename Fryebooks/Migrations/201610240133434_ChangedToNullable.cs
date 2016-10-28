namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alerts", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes");
            DropForeignKey("dbo.Expenses", "ClientId", "dbo.Clients");
            DropIndex("dbo.Alerts", new[] { "ClientId" });
            DropIndex("dbo.WorkResponses", new[] { "IncomeId" });
            DropIndex("dbo.Expenses", new[] { "ClientId" });
            AlterColumn("dbo.Alerts", "ClientId", c => c.Int());
            AlterColumn("dbo.WorkResponses", "IncomeId", c => c.Int());
            AlterColumn("dbo.Expenses", "ClientId", c => c.Int());
            CreateIndex("dbo.Alerts", "ClientId");
            CreateIndex("dbo.WorkResponses", "IncomeId");
            CreateIndex("dbo.Expenses", "ClientId");
            AddForeignKey("dbo.Alerts", "ClientId", "dbo.Clients", "Id");
            AddForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes", "Id");
            AddForeignKey("dbo.Expenses", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes");
            DropForeignKey("dbo.Alerts", "ClientId", "dbo.Clients");
            DropIndex("dbo.Expenses", new[] { "ClientId" });
            DropIndex("dbo.WorkResponses", new[] { "IncomeId" });
            DropIndex("dbo.Alerts", new[] { "ClientId" });
            AlterColumn("dbo.Expenses", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkResponses", "IncomeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Alerts", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Expenses", "ClientId");
            CreateIndex("dbo.WorkResponses", "IncomeId");
            CreateIndex("dbo.Alerts", "ClientId");
            AddForeignKey("dbo.Expenses", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Alerts", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
