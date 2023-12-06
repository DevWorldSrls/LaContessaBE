﻿using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Booking;

public class CreateBooking : IRequest
{
    public BookingDetail Booking { get; set; } = null!;
       
    public class BookingDetail
    {
        public string UserId { get; set; }
        public string Date { get; set; }
        public string activityID { get; set; }
        public string timeSlot { get; set; }
        public string bookingName { get; set; }
        public string phoneNumber { get; set; }
        public double price { get; set; }
        public bool IsLesson { get; set; }
    }
}
