namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationToClient : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Clients", "ImageUrl", c => c.String());
            //AddColumn("dbo.Clients", "Location", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.Clients", "Location");
            //DropColumn("dbo.Clients", "ImageUrl");
        }
    }
}
