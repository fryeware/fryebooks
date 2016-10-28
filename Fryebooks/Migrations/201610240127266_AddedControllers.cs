namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedControllers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpenseAmount = c.Double(nullable: false),
                        Refundable = c.Boolean(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "ClientId", "dbo.Clients");
            DropIndex("dbo.Expenses", new[] { "ClientId" });
            DropTable("dbo.Expenses");
        }
    }
}
