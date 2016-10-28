namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReversedIncomeToWRRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes");
            DropIndex("dbo.WorkResponses", new[] { "IncomeId" });
            AddColumn("dbo.Incomes", "WorkResponseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Incomes", "WorkResponseId");
            AddForeignKey("dbo.Incomes", "WorkResponseId", "dbo.WorkResponses", "Id", cascadeDelete: true);
            DropColumn("dbo.WorkResponses", "IncomeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkResponses", "IncomeId", c => c.Int());
            DropForeignKey("dbo.Incomes", "WorkResponseId", "dbo.WorkResponses");
            DropIndex("dbo.Incomes", new[] { "WorkResponseId" });
            DropColumn("dbo.Incomes", "WorkResponseId");
            CreateIndex("dbo.WorkResponses", "IncomeId");
            AddForeignKey("dbo.WorkResponses", "IncomeId", "dbo.Incomes", "Id");
        }
    }
}
