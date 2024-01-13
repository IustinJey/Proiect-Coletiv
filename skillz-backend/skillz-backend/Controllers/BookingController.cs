using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillz_backend.DTOs;
using skillz_backend.models;
using skillz_backend.Services.Interfaces;

namespace skillz_backend.Controllers
{
    [ApiController]
    [Route("booking/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            if (bookingId <= 0)
            {
                return BadRequest("Invalid BookingId. It should be a positive integer.");
            }

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            return Ok(bookings);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetBookingsByClient(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest("Invalid ClientId. It should be a positive integer.");
            }

            var bookings = await _bookingService.GetBookingsByClientAsync(clientId);

            if (bookings.Count == 0)
            {
                return NotFound($"No bookings found for client with ClientId '{clientId}'.");
            }

            return Ok(bookings);
        }

        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetBookingsByProvider(int providerId)
        {
            if (providerId <= 0)
            {
                return BadRequest("Invalid ProviderId. It should be a positive integer.");
            }

            var bookings = await _bookingService.GetBookingsByProviderAsync(providerId);

            if (bookings.Count == 0)
            {
                return NotFound($"No bookings found for provider with ProviderId '{providerId}'.");
            }

            return Ok(bookings);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetBookingsByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be null or empty.");
            }

            var bookings = await _bookingService.GetBookingsByStatusAsync(status);

            if (bookings.Count == 0)
            {
                return NotFound($"No bookings found with the status '{status}'.");
            }

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Manual mapping from BookingDto to Booking
                var booking = new Booking
                {
                    ClientUserId = bookingDto.ClientUserId,
                    ProviderUserId = bookingDto.ProviderUserId,
                    DateTime = bookingDto.DateTime,
                    Details = bookingDto.Details,
                    Status = Enum.Parse<BookingStatus>(bookingDto.Status, true) // Ignoră case sensitivity
                };

                await _bookingService.CreateBookingAsync(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new BookingDto
            {
                ClientUserId = bookingDto.ClientUserId,
                ProviderUserId = bookingDto.ProviderUserId,
                DateTime = bookingDto.DateTime,
                Details = bookingDto.Details,
                Status = bookingDto.Status
            };
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBooking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (existingBooking == null)
            {
                return NotFound($"Booking with BookingId '{bookingId}' not found.");
            }

            try
            {
                // Manual mapping from BookingDto to Booking
                existingBooking.ClientUserId = bookingDto.ClientUserId;
                existingBooking.ProviderUserId = bookingDto.ProviderUserId;
                existingBooking.DateTime = bookingDto.DateTime;
                existingBooking.Details = bookingDto.Details;
                existingBooking.Status = Enum.Parse<BookingStatus>(bookingDto.Status, true); // Ignoră case sensitivity

                await _bookingService.UpdateBookingAsync(existingBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(bookingDto);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                return BadRequest("Invalid BookingId. It should be a positive integer.");
            }

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound($"Booking with BookingId '{bookingId}' not found.");
            }

            await _bookingService.DeleteBookingAsync(bookingId);

            return NoContent();
        }
    }

}