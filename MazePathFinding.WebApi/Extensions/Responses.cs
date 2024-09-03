namespace MazePathFinding.WebApi.Extensions;

public static class Responses
{
    public static object? Success(this string text) => new { solution = text };

    public static object? Error(this string text) => new { error = text };
}
