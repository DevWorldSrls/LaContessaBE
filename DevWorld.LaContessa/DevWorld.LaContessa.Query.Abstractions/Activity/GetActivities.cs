using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetActivities : IRequest<GetActivities.Response>
{
    public class Response
    {
        public ActivityDetail[] Activities { get; set; }

        public class ActivityDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool IsAvaible { get; set; }
            public string Type { get; set; }
        }
    }
}