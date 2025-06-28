using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HotelManagement.Models
{
    public class Guest
    {
        public int GuestId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public int RoomId { get; set; } // Foreign key

        [Display(Name = "Room")]
        public virtual Room Room { get; set; } // Navigation property


        [Display(Name = "Check-In Date")]
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }

        [Display(Name = "Check-Out Date")]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }




        //public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}