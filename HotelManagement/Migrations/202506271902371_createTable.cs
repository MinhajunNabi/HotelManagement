namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "IsAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "IsAvailable", c => c.Boolean(nullable: false));
        }
    }
}
