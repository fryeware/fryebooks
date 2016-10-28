namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedidIncomeToWRRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Incomes", "WorkResponseId", "dbo.WorkResponses");
            DropIndex("dbo.Incomes", new[] { "WorkResponseId" });
            AddColumn("dbo.WorkResponses", "IncomeId", c => c.Int());
            CreateIndex("dbo.WorkResponses", "IncomeId");
            AddForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes", "Id");
            DropColumn("dbo.Incomes", "WorkResponseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incomes", "WorkResponseId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes");
            DropIndex("dbo.WorkResponses", new[] { "IncomeId" });
            DropColumn("dbo.WorkResponses", "IncomeId");
            CreateIndex("dbo.Incomes", "WorkResponseId");
            AddForeignKey("dbo.Incomes", "WorkResponseId", "dbo.WorkResponses", "Id", cascadeDelete: true);
        }
    }
}
