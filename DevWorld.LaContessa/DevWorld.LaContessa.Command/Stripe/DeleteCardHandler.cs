using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Stripe;

public class DeleteCardHandler : IRequestHandler<DeleteCard>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IStripeAppService _stripeAppService;

    public DeleteCardHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
    }

    public async Task Handle(DeleteCard request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        await _stripeAppService.DeleteStripePaymentAsync(
            userToUpdate.PaymentMethodId ?? throw new PaymentMethodNotFoundException(),
            cancellationToken);

        userToUpdate.PaymentMethodId = null;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
