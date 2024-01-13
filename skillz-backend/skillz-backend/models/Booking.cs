using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int ClientUserId { get; set; }
        public User ClientUser { get; set; }
        public int ProviderUserId { get; set; }
        public User ProviderUser { get; set; }
        public DateTime DateTime { get; set; }
        public string Details { get; set; }
        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
