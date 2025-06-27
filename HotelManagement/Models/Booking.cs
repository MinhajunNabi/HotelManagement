using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
    }
}