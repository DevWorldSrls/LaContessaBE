using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Booking;

public class UpdateActivity : IRequest
{
    public ActivityDetail? Activity { get; set; }
       
    public class ActivityDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsAvaible { get; set; }
        public string Descripting { get; set; }
        public string[] Services { get; set; }
        public string[] Dates { get; set; }
    }
}
