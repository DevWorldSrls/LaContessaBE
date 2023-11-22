using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetActivity : IRequest<GetActivity.Response>
{
    public Guid Id { get; set; }

    public GetActivity(Guid id)
    {
        Id = id;
    }

    public class Response
    {
       public ActivityDetail? Activity { get; set; }
       
       public class ActivityDetail
       {
           public Guid Id { get; set; }
           public string Name { get; set; }
           public bool IsAvaible { get; set; }
           public string Type { get; set; }
       }
    }

}
