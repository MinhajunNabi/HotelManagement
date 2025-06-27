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
        public string FoodItem { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
