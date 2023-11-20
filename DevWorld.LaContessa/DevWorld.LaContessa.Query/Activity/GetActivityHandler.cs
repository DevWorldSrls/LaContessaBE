using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetActivityHandler : IRequestHandler<GetActivity, GetActivity.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetActivity.Response> Handle(GetActivity request, CancellationToken cancellationToken)
    {
        return new GetActivity.Response
        {
            Booking = await _laContessaDbContext.Activities
                .Where(x => x.Id == request.Id)
                .Select(x => new GetActivity.Response.ActivityDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsAvaible = x.IsAvaible,
                    Type = x.Type
                })
                .FirstOrDefaultAsync(),
        };
    }
}