using System;
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        // UserId for the client (the person making the reservation)
        public int ClientUserId { get; set; }
        public User ClientUser { get; set; }

        // UserId for the provider (the worker)
        public int ProviderUserId { get; set; }
        public User ProviderUser { get; set; }

        public DateTime DateTime { get; set; } //Any format is accepted, and both date and time can be stored.
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
