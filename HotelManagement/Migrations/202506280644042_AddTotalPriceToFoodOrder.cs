namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalPriceToFoodOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrders", "SelectedItems", c => c.String(nullable: false));
            AddColumn("dbo.FoodOrders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.FoodOrders", "FoodItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodOrders", "FoodItem", c => c.String(nullable: false));
            DropColumn("dbo.FoodOrders", "TotalPrice");
            DropColumn("dbo.FoodOrders", "SelectedItems");
        }
    }
}
