using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Activities;

public class DeleteActivityHandler : IRequestHandler<DeleteActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public DeleteActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(DeleteActivity request, CancellationToken cancellationToken)
    {
        var activityToUpdate = await _laContessaDbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken) ?? throw new ActivityNotFoundException();

        _laContessaDbContext.Activities.Remove(activityToUpdate);

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}