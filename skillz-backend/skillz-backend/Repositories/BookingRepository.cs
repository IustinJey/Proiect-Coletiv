using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories.Interfaces;

namespace skillz_backend.Repositories
{
    public class BookingRepository : IBookingRepository
{
    private readonly SkillzDbContext _dbContext;

    public BookingRepository(SkillzDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Booking> GetBookingByIdAsync(int bookingId)
    {
        return await _dbContext.Bookings.FindAsync(bookingId);
    }

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByClientAsync(int clientId)
    {
        return await _dbContext.Bookings.Where(b => b.ClientUserId == clientId).ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByProviderAsync(int providerId)
    {
        return await _dbContext.Bookings.Where(b => b.ProviderUserId == providerId).ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status)
    {
        return await _dbContext.Bookings.Where(b => b.Status == status).ToListAsync();
    }

    public async Task CreateBookingAsync(Booking booking)
    {
        _dbContext.Bookings.Add(booking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        _dbContext.Entry(booking).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookingAsync(int bookingId)
    {
        var bookingToDelete = await _dbContext.Bookings.FindAsync(bookingId);
        if (bookingToDelete != null)
        {
            _dbContext.Bookings.Remove(bookingToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}

}