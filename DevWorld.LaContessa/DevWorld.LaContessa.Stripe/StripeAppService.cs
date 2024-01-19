using DevWorld.LaContessa.Stripe.Abstractions.Cards;
using DevWorld.LaContessa.Stripe.Abstractions.Customers;
using DevWorld.LaContessa.Stripe.Abstractions.Payments;
using Stripe;

namespace DevWorld.LaContessa.Stripe
{
    public class StripeAppService : IStripeAppService
    {
        private readonly PaymentIntentService _paymentIntentService;
        private readonly CustomerService _customerService;
        private readonly RefundService _refundService;
        private readonly PaymentMethodService _paymentMethodService;

        public StripeAppService(
            PaymentIntentService paymentIntentService,
            CustomerService customerService,
            RefundService refundService,
            PaymentMethodService paymentMethodService)
        {
            _paymentIntentService = paymentIntentService;
            _customerService = customerService;
            _refundService = refundService;
            _paymentMethodService = paymentMethodService;
        }

        /// <summary>
        /// Add a new card at Stripe using Customer.
        /// If Customer doesn't exist, it will create it.
        /// </summary>
        /// <returns><Stripe Customer Updated/returns>
        public async Task<StripeCustomer> AddStripeCustomerCard(CreateStripeCard card, string? customerId = null, CreateStripeCustomer? customer = null, CancellationToken ct = default)
        {
            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = card.CardNumber,
                    ExpYear = card.ExpirationYear,
                    ExpMonth = card.ExpirationMonth,
                    Cvc = card.Cvc
                }
            };

            if(customerId is null && customer is not null)
            {
                var newCustomer = await CreateStripeCustomerAsync(customer, ct);
                customerId = newCustomer.CustomerId;
            }

            // Create Payment Method
            var paymentMethod = await _paymentMethodService.CreateAsync(paymentMethodOptions, null, ct); 
            
            var paymentMethodAttachOption = new PaymentMethodAttachOptions
            {
                Customer = customerId,
            };

            // Attach Payment Method to Customer
            var attachPaymentResponse = await _paymentMethodService.AttachAsync(paymentMethod.Id, paymentMethodAttachOption, null, ct);

            var updatedCustomer = await _customerService.UpdateAsync(customerId, new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = attachPaymentResponse.Id,
                }
            }, null, ct);

            // Return the created customer at stripe
            return new StripeCustomer
            {
                Name = updatedCustomer.Name,
                Email = updatedCustomer.Email,
                CustomerId = updatedCustomer.Id,
                PaymentMethodId = attachPaymentResponse.Id
            };
        }

        /// <summary>
        /// Add a new payment at Stripe using Customer and Payment details.
        /// Customer has to exist at Stripe already.
        /// </summary>
        /// <param name="payment">Stripe Payment</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns><Stripe Payment/returns>
        public async Task<StripePayment> CreateStripePaymentAsync(CreateStripePayment payment, CancellationToken ct)
        {
            // Set the options for the payment we would like to create at Stripe
            var paymentOptions = new PaymentIntentCreateOptions
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            // Create the payment
            var createdPayment = await _paymentIntentService.CreateAsync(paymentOptions, null, ct);

            var paymentMethod = await _customerService.RetrievePaymentMethodAsync(payment.CustomerId, payment.PaymentMethodId, null, null, ct);

            if (paymentMethod == null) return new StripePayment { };

            var confirmPaymentOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethod!.Id,
                ReturnUrl = "https://www.example.com"
            };

            // Confirm the payment
            var confirmedPayment = await _paymentIntentService.ConfirmAsync(createdPayment.Id, confirmPaymentOptions);

            // Return the payment to requesting method
            return new StripePayment {
                CustomerId = confirmedPayment.CustomerId,
                ReceiptEmail = confirmedPayment.ReceiptEmail,
                Description = confirmedPayment.Description,
                Currency = confirmedPayment.Currency,
                Amount = confirmedPayment.Amount,
                PaymentId = confirmedPayment.Id
            };
        }

        /// <summary>
        /// Detach payment at Stripe using Customer and Payment details.
        /// Customer has to exist at Stripe already.
        /// </summary>
        public async Task DeleteStripePaymentAsync(string paymentId, CancellationToken ct)
        {
            // Detach Payment Method from Customer
            await _paymentMethodService.DetachAsync(paymentId, null, null, ct);

            return;
        }

        /// <summary>
        /// Refund payment at Stripe using PaymentIntent details.
        /// PaymentIntent has to exist at Stripe already.
        /// </summary>
        public async Task RefundStripePaymentAsync(string paymentIntentId, CancellationToken ct)
        {
            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId
            };

            await _refundService.CreateAsync(refundOptions, null, ct);

            return;
        }

        /// <summary>
        /// Create a new customer at Stripe through API using customer and card details from records.
        /// </summary>
        /// <param name="customer">Stripe Customer</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Stripe Customer</returns>
        private async Task<StripeCustomer> CreateStripeCustomerAsync(CreateStripeCustomer customer, CancellationToken ct)
        {
            // Set Customer options using
            var customerOptions = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email
            };

            // Create customer at Stripe
            var createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            // Return the created customer at stripe
            return new StripeCustomer
            {
                Name = createdCustomer.Name,
                Email = createdCustomer.Email,
                CustomerId = createdCustomer.Id
            };
        }
    }
}
