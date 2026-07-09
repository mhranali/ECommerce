using System.Text.Json.Serialization;

namespace ECommerce.UseCases.Common;

public sealed record Error(string Code, string Description,ErrorType ErrorType = ErrorType.Failure)
{
    
    public static Error Failure(string code = "General.Failure", string description = "General Failure has Occurred")
        => new (code, description, ErrorType.Failure);
    public static Error Validation(string code = "General.Validation", string description = "General Validation Error has Occurred")
        => new (code, description, ErrorType.Validation);
    public static Error NotFound(string code = "General.NotFound",string description = "The Requested Resource Was Not Found")
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code = "General.Conflict", string description = "A Conflict Has Occurred")
        => new(code, description, ErrorType.Conflict);

    public static Error Unauthorized(string code = "General.Unauthorized",string description = "Unauthorized Access")
        => new(code, description, ErrorType.Unauthorized);

    public static Error Forbidden(string code = "General.Forbidden",string description = "Access To This Resource Is Forbidden")
        => new(code, description, ErrorType.Forbidden);

    public static Error InvalidCredentials(string code = "General.InvalidCredentials",string description = "Invalid Username Or Password")
        => new(code, description, ErrorType.InvalidCredentials);

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Unauthorized = 4,
    Forbidden = 5,
    InvalidCredentials = 6

}