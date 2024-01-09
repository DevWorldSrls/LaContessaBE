using DevWorld.LaContessa.Stripe.Abstractions.Customers;
using DevWorld.LaContessa.Stripe.Abstractions.Payments;
using Stripe;

namespace DevWorld.LaContessa.Stripe
{
    public class StripeAppService : IStripeAppService
    {
        private readonly PaymentIntentService _paymentIntentService;
        private readonly CustomerService _customerService;
        private readonly PaymentMethodService _paymentMethodService;

        public StripeAppService(
            PaymentIntentService paymentIntentService,
            CustomerService customerService,
            PaymentMethodService paymentMethodService)
        {
            _paymentIntentService = paymentIntentService;
            _customerService = customerService;
            _paymentMethodService = paymentMethodService;
        }

        /// <summary>
        /// Create a new customer at Stripe through API using customer and card details from records.
        /// </summary>
        /// <param name="customer">Stripe Customer</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Stripe Customer</returns>
        public async Task<StripeCustomer> CreateStripeCustomerAsync(CreateStripeCustomer customer, CancellationToken ct)
        {
            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = customer.CreditCard.CardNumber,
                    ExpYear = customer.CreditCard.ExpirationYear,
                    ExpMonth = customer.CreditCard.ExpirationMonth,
                    Cvc = customer.CreditCard.Cvc
                }
            };

            // Create Payment Method
            var paymentMethod = await _paymentMethodService.CreateAsync(paymentMethodOptions, null, ct);

            // Set Customer options using
            var customerOptions = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email
            };

            // Create customer at Stripe
            var createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            var paymentMethodAttachOption = new PaymentMethodAttachOptions
            {
                Customer = createdCustomer.Id,
            };

            // Attach Payment Method to Customer
            var attachPaymentResponse = await _paymentMethodService.AttachAsync(paymentMethod.Id, paymentMethodAttachOption, null, ct);

            var updatedCustomer = await _customerService.UpdateAsync(createdCustomer.Id, new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = attachPaymentResponse.Id,
                }
            }, null, ct);

            // Return the created customer at stripe
            return new StripeCustomer {
                Name = updatedCustomer.Name,
                Email = updatedCustomer.Email,
                CustomerId = updatedCustomer.Id
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

            var paymentMethod = await _customerService.ListPaymentMethodsAsync(payment.CustomerId, new CustomerListPaymentMethodsOptions { Limit = 1 });

            if (paymentMethod == null || !paymentMethod.Any()) return new StripePayment { };

            var confirmPaymentOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethod!.First().Id,
                ReturnUrl = "https://www.example.com"
            };

            // Confirm the payment
            var confirmedPayment = await _paymentIntentService.ConfirmAsync(createdPayment.Id, confirmPaymentOptions);

            // Return the payment to requesting method
            return new StripePayment {
                CustomerId = createdPayment.CustomerId,
                ReceiptEmail = createdPayment.ReceiptEmail,
                Description = createdPayment.Description,
                Currency = createdPayment.Currency,
                Amount = createdPayment.Amount,
                PaymentId = createdPayment.Id
            };
        }
    }
}
