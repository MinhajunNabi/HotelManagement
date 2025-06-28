namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class room : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guests", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Guests", new[] { "RoomId" });
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuestName = c.String(nullable: false),
                        RoomType = c.String(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            
            
            
            
            AddColumn("dbo.Rooms", "Guest_GuestId", c => c.Int());
            CreateIndex("dbo.Rooms", "Guest_GuestId");
            AddForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests", "GuestId");
            DropColumn("dbo.Guests", "RoomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "RoomId", c => c.Int());
            DropForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests");
            DropIndex("dbo.Rooms", new[] { "Guest_GuestId" });
            DropColumn("dbo.Rooms", "Guest_GuestId");
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
            CreateIndex("dbo.Guests", "RoomId");
            AddForeignKey("dbo.Guests", "RoomId", "dbo.Rooms", "RoomId");
        }
    }
}
