namespace DevWorld.LaContessa.Domain.Entities.Activities;

public class Activity : SoftDeletable
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string[] Services { get; set; }
    public string[] Dates { get; set; }
    public bool IsAvaible { get; set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Activity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

