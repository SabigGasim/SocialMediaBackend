﻿namespace SocialMediaBackend.BuildingBlocks.Tests;

public class Timeout
{
    private readonly DateTime _endTime;

    public Timeout(int duration)
    {
        this._endTime = DateTime.Now.AddMilliseconds(duration);
    }

    public bool HasTimedOut()
    {
        return DateTime.Now > _endTime;
    }
}