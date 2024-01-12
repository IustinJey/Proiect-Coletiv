using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.models;
using skillz_backend.Repositories;
using skillz_backend.Repositories.Interfaces;
using skillz_backend.Services.Interfaces;

namespace skillz_backend.Services
{
    public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;

    public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Booking> GetBookingByIdAsync(int bookingId)
    {
        if (bookingId <= 0)
        {
            throw new ArgumentException("BookingId should be a positive integer.");
        }

        return await _bookingRepository.GetBookingByIdAsync(bookingId);
    }

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        return await _bookingRepository.GetAllBookingsAsync();
    }

    public async Task<List<Booking>> GetBookingsByClientAsync(int clientId)
    {
        if (clientId <= 0)
        {
            throw new ArgumentException("ClientId should be a positive integer.");
        }

        return await _bookingRepository.GetBookingsByClientAsync(clientId);
    }

    public async Task<List<Booking>> GetBookingsByProviderAsync(int providerId)
    {
        if (providerId <= 0)
        {
            throw new ArgumentException("ProviderId should be a positive integer.");
        }

        return await _bookingRepository.GetBookingsByProviderAsync(providerId);
    }

    public async Task<List<Booking>> GetBookingsByStatusAsync(string status)
    {
        // Validare și conversie a statusului
        if (!Enum.TryParse<BookingStatus>(status, true, out var bookingStatus))
        {
            throw new ArgumentException($"Invalid BookingStatus: {status}");
        }

        return await _bookingRepository.GetBookingsByStatusAsync(bookingStatus);
    }

    public async Task CreateBookingAsync(Booking booking)
    {
        // Validări pentru Booking
        ValidateBooking(booking);

        // Verifică dacă utilizatorul client există
        var existingClient = await _userRepository.GetUserByIdAsync(booking.ClientUserId);
        if (existingClient == null)
        {
            throw new InvalidOperationException($"User with Id {booking.ClientUserId} does not exist.");
        }

        // Verifică dacă utilizatorul provider există
        var existingProvider = await _userRepository.GetUserByIdAsync(booking.ProviderUserId);
        if (existingProvider == null)
        {
            throw new InvalidOperationException($"User with Id {booking.ProviderUserId} does not exist.");
        }

        await _bookingRepository.CreateBookingAsync(booking);
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        // Validări pentru Booking
        ValidateBooking(booking);

        // Verifică dacă utilizatorul client există
        var existingClient = await _userRepository.GetUserByIdAsync(booking.ClientUserId);
        if (existingClient == null)
        {
            throw new InvalidOperationException($"User with Id {booking.ClientUserId} does not exist.");
        }

        // Verifică dacă utilizatorul provider există
        var existingProvider = await _userRepository.GetUserByIdAsync(booking.ProviderUserId);
        if (existingProvider == null)
        {
            throw new InvalidOperationException($"User with Id {booking.ProviderUserId} does not exist.");
        }

        await _bookingRepository.UpdateBookingAsync(booking);
    }

    public async Task DeleteBookingAsync(int bookingId)
    {
        if (bookingId <= 0)
        {
            throw new ArgumentException("BookingId should be a positive integer.");
        }

        await _bookingRepository.DeleteBookingAsync(bookingId);
    }

    private static void ValidateBooking(Booking booking)
    {
        if (booking == null)
        {
            throw new ArgumentNullException(nameof(booking), "Booking object cannot be null.");
        }

        // Alte validări specifice pentru Booking
    }
}
}