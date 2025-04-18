using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Bookings;

public class GetBookings : IRequest<GetBookings.Response>
{
    public class Response
    {
        public BookingDetail[] Bookings { get; set; } = null!;

        public class BookingDetail
        {
            public Guid Id { get; set; }
            public UserDetail User { get; set; } = null!;
            public ActivityDetail Activity { get; set; } = null!;
            public string Date { get; set; } = null!;
            public string TimeSlot { get; set; } = null!;
            public string BookingName { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public bool IsLesson { get; set; } = false;
            public BookingStatus Status { get; set; }
            public long BookingPrice { get; set; }
            public long? PaymentPrice { get; set; }

            public class UserDetail
            {
                public Guid Id { get; set; }
                public string Name { get; set; } = null!;
                public string Surname { get; set; } = null!;
                public string Email { get; set; } = null!;
                public bool IsPro { get; set; } = false;
                public bool PeriodicBookingsEnabled { get; set; } = false;
                public string? ImageProfile { get; set; }
                public string? CardNumber { get; set; }
                public bool HasCreditCardLinked { get; set; } = false;
                public bool IsDeleted { get; set; } = false;
            }

            public class ActivityDetail
            {
                public Guid Id { get; set; }
                public string Name { get; set; } = null!;
                public bool IsOutdoor { get; set; }
                public bool IsSubscriptionRequired { get; set; }
                public double? Price { get; set; }
                public ActivityBookingType BookingType { get; set; }
                public string Duration { get; set; } = null!;
                public int? Limit { get; set; }
                public string? Description { get; set; }
                public string? ActivityImg { get; set; }
                public string? ExpirationDate { get; set; }
            }
        }
    }
}