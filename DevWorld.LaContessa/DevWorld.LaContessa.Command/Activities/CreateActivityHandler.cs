using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Activity;

public class CreateActivityHandler : IRequestHandler<CreateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateActivity request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Activities.Where(x => request.Activity.Id == x.Id).AnyAsync();

        if (alreadyExist)
            throw new ActivityAlreadyExistException();

        var activityToAdd = new Domain.Entities.Activities.Activity 
        { 
            Id = Guid.NewGuid(),
            Name = request.Activity.Name,
            Type = request.Activity.Type,
            Description = request.Activity.Descripting,
            Services = request.Activity.Services,
            Dates = request.Activity.Dates,
        };


        await _laContessaDbContext.AddAsync(activityToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
