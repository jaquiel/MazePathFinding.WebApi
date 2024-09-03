using MazePathFinding.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MazePathFinding.WebApi.Extensions;

public static class Requests
{
    private const int MaxRows = 20;
    private const int MaxColumns = 20;

    public static string Validate(this MazeRequest request)
    {
        if (request?.Grid == null || request.Grid.Count == 0)
        {
            return "Invalid maze grid.";
        }

        if (request.Grid.Count > MaxRows || request.Grid.Exists(row => row.Count > MaxColumns))
        {
            return $"Maze exceeds maximum size of {MaxRows} rows x {MaxColumns} columns.";
        }

        if (!request.Grid.HasRowsTheSameNumberOfColumns())
        {
            return "Maze must have the same number of columns in each row.";
        }

        return string.Empty;
    }

    private static bool HasRowsTheSameNumberOfColumns(this List<List<char>> grid)
    {
        int columnCount = grid[0].Count;

        foreach (var row in grid)
        {
            if (row.Count != columnCount)
                return false;
        }

        return true;
    }
}
