using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required]
        public string SelectedItems { get; set; } // e.g. "Pizza,Burger"

        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }


}
