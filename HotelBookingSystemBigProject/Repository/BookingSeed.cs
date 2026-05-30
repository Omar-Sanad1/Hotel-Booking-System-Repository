using Core.Entities;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingSeed
    {
        public static async Task SeedAsync(HotelBookingDbContext dbContext)
        {
            ///////////////////////////////////////////////////////////////////
            // Hotels
            if (!dbContext.Hotels.Any())
            {
                var hotelsData = File.ReadAllText("../Repository/DataSeed/hotels.json");
                var hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsData);
                if(hotels?.Count > 0)
                {
                    foreach(var hotel in hotels)
                    {
                        await dbContext.AddAsync(hotel);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }

            ///////////////////////////////////////////////////////////////////
            // Rooms
            if (!dbContext.Rooms.Any())
            {
                var roomsData = File.ReadAllText("../Repository/DataSeed/rooms.json");
                var rooms = JsonSerializer.Deserialize<List<Room>>(roomsData);
                if(rooms?.Count > 0)
                {
                    foreach(var room in rooms)
                    {
                        await dbContext.AddAsync(room);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///////////////////////////////////////////////////////////////////
            // Customers
            if (!dbContext.Customers.Any())
            {
                var customersData = File.ReadAllText("../Repository/DataSeed/customers.json");
                var customers = JsonSerializer.Deserialize<List<Customer>>(customersData);
                if(customers?.Count > 0)
                {
                    foreach(var customer in customers)
                    {
                        customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
                        await dbContext.AddAsync(customer);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///////////////////////////////////////////////////////////////////
            // Reservations
            if (!dbContext.Reservations.Any())
            {
                var reservationData = File.ReadAllText("../Repository/DataSeed/reservations.json");
                var reservations = JsonSerializer.Deserialize<List<Reservation>>(reservationData);
                if(reservations?.Count > 0)
                {
                    foreach (var reservation in reservations)
                    {
                        await dbContext.AddAsync(reservation);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///////////////////////////////////////////////////////////////////
            // Reviews
            if (!dbContext.Reviews.Any())
            {
                var reviewData = File.ReadAllText("../Repository/DataSeed/reviews.json");
                var reviews = JsonSerializer.Deserialize<List<Review>>(reviewData);
                if (reviews?.Count > 0)
                {
                    foreach (var review in reviews)
                    {
                        await dbContext.AddAsync(review);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///////////////////////////////////////////////////////////////////
            // Payments
            if (!dbContext.Payments.Any())
            {
                var paymentData = File.ReadAllText("../Repository/DataSeed/payments.json");
                var payments = JsonSerializer.Deserialize<List<Payment>>(paymentData);
                if (payments?.Count > 0)
                {
                    foreach (var payment in payments)
                    {
                        await dbContext.AddAsync(payment);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
