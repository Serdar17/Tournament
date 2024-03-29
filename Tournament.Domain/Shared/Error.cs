﻿namespace Tournament.Domain.Shared;

public class Error
{
    public string Field { get; init; }

    public string Message { get; init; }

    public Error(string field, string message)
    {
        Field = field;
        Message = message;
    }
}