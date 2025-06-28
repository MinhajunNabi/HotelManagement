namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCheckInCheckOutDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "CheckInDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Guests", "CheckOutDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "CheckOutDate");
            DropColumn("dbo.Guests", "CheckInDate");
        }
    }
}
