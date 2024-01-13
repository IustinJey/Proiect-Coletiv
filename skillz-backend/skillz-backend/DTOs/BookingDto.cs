using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skillz_backend.DTOs
{
    public class BookingDto
    {
        [Required]
        public int ClientUserId { get; set; }

        [Required]
        public int ProviderUserId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string Details { get; set; }

        [Required]
        public string Status { get; set; }
    }

}