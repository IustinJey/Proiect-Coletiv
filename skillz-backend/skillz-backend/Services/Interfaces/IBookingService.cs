using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByClientAsync(int clientId);
        Task<List<Booking>> GetBookingsByProviderAsync(int providerId);
        Task<List<Booking>> GetBookingsByStatusAsync(string status);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
    }
}