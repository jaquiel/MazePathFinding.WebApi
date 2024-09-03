namespace MazePathFinding.WebApi.Helpers;

public static class Helper
{
    public static (int x, int y)? FindPoint(char[,] grid, char point)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == point)
                {
                    return (i, j);
                }
            }
        }

        return null;
    }

    public static char[,] ConvertArrayFromJaggedToMultidimensional(List<List<char>> grid)
    {
        int rows = grid.Count;
        int cols = grid[0].Count;

        char[,] gridArray = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                gridArray[i, j] = grid[i][j];
            }
        }

        return gridArray;
    }

    public static string GetFormattedAsString(List<int[]> path)
    {
        string? formattedPath = string.Empty;

        foreach (var point in path)
        {
            formattedPath += $" -> [{point[0]},{point[1]}]";
        }

        return $"S {formattedPath} -> G";
    }
}
