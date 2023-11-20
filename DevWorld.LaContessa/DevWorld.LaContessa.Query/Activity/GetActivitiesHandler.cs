using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetActivitiesHandler : IRequestHandler<GetActivities, GetActivities.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetActivitiesHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetActivities.Response> Handle(GetActivities request, CancellationToken cancellationToken)
    {
        return new GetActivities.Response
        { 
            Activities = await _laContessaDbContext.Activities
                .AsQueryable()
                .Select(x => new GetActivities.Response.ActivityDetail
            {
                Id = x.Id,
                Name = x.Name,
                IsAvaible = x.IsAvaible,
                Type = x.Type
            }).ToArrayAsync()
        };
    }
}
