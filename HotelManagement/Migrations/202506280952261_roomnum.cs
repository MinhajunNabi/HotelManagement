namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomnum : DbMigration
    {
        public override void Up()
        {
            // OPTIONAL: Ensure RoomId = 1 exists BEFORE setting foreign key constraint
            Sql("SET IDENTITY_INSERT dbo.Rooms ON");
            Sql("INSERT INTO dbo.Rooms (RoomId, RoomNumber, Type, PricePerNight, IsAvailable) " +
                "VALUES (1, '101', 'Single', 1000, 1)");
            Sql("SET IDENTITY_INSERT dbo.Rooms OFF");

            // 1. Remove old FK (if any)
            DropForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests");
            DropIndex("dbo.Rooms", new[] { "Guest_GuestId" });

            // 2. Add new RoomId column to Guests with default value 1
            AddColumn("dbo.Guests", "RoomId", c => c.Int(nullable: false, defaultValue: 1));

            // 3. Create FK and index
            CreateIndex("dbo.Guests", "RoomId");
            AddForeignKey("dbo.Guests", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);

            // 4. Drop old column from Rooms
            DropColumn("dbo.Rooms", "Guest_GuestId");
        }


        public override void Down()
        {
            AddColumn("dbo.Rooms", "Guest_GuestId", c => c.Int());
            DropForeignKey("dbo.Guests", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Guests", new[] { "RoomId" });
            DropColumn("dbo.Guests", "RoomId");
            CreateIndex("dbo.Rooms", "Guest_GuestId");
            AddForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests", "GuestId");
        }
    }
}
