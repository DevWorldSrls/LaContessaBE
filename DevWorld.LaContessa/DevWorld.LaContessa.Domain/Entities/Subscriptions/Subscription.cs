﻿namespace DevWorld.LaContessa.Domain.Entities.Subscriptions;

public class Subscription : SoftDeletable
{
    public string UserId { get; set; }
    public int CardNumber { get; set; }
    public bool Valid { get; set; }
    public string ExpirationDate { get; set; }
    public string SubscriptionType { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Subscription() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
