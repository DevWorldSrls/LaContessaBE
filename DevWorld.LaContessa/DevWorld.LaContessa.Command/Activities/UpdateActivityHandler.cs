using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Booking;

public class UpdateActivityHandler : IRequestHandler<UpdateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateActivity request, CancellationToken cancellationToken)
    {
        var activityToUpdate = await _laContessaDbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.Activity.Id && !x.IsDeleted);

        if (activityToUpdate is null)
            throw new UserNotFoundException();

        activityToUpdate.Name = request.Activity.Name;
        activityToUpdate.Type = request.Activity.Type;
        activityToUpdate.Description = request.Activity.Descripting;
        activityToUpdate.Services = request.Activity.Services;
        activityToUpdate.Dates = request.Activity.Dates;

        await _laContessaDbContext.SaveChangesAsync();
    }
}
