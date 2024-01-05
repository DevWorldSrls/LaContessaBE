﻿using MediatR;

namespace DevWorld.LaContessa.Stripe.Abstractions.Payments
{
    public class CreateStripePayment
    {
        public string CustomerId { get; set; } = null!;
        public string ReceiptEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public long Amount { get; set; }
    }
}
