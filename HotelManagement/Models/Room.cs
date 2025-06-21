using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HotelManagement.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [Display(Name = "Room Number")]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [Display(Name = "Room Type")]
        [StringLength(50)]
        public string Type { get; set; }  // Single, Double, etc.

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 100000)]
        [Display(Name = "Price Per Night")]
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        //public virtual ICollection<RoomBooking> RoomBookings { get; set; }

    }
}