using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByClientAsync(int clientId);
        Task<List<Booking>> GetBookingsByProviderAsync(int providerId);
        Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
    }
}