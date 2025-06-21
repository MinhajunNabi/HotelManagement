namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestTableforce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Phone = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.GuestId);
            
            AddColumn("dbo.Rooms", "Guest_GuestId", c => c.Int());
            CreateIndex("dbo.Rooms", "Guest_GuestId");
            AddForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests", "GuestId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests");
            DropIndex("dbo.Rooms", new[] { "Guest_GuestId" });
            DropColumn("dbo.Rooms", "Guest_GuestId");
            DropTable("dbo.Guests");
        }
    }
}
