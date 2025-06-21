namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(nullable: false, maxLength: 10),
                        Type = c.String(nullable: false, maxLength: 50),
                        PricePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
        }
    }
}
