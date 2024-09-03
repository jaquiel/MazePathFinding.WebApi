using MazePathFinding.WebApi.Services;
using MazePathfindingAPI.WebApi.Models;

namespace MazePathfindingAPI.Services;

public class MazesService : IMazesService
{
    public List<int[]> SolveMaze(Maze maze)
    {
        var start = maze.Start;
        var goal = maze.Goal;
        var grid = maze.Grid;
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        var queue = new Queue<(int x, int y)>();
        var visited = new HashSet<(int x, int y)>();
        var parent = new Dictionary<(int x, int y), (int x, int y)?>();
        parent[start] = null;

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == goal)
            {
                return BuildPath(parent, current);
            }

            foreach (var move in GetValidMoves(current, grid, rows, cols))
            {
                if (!visited.Contains(move))
                {
                    queue.Enqueue(move);
                    visited.Add(move);
                    parent[move] = current;
                }
            }
        }

        return null;
    }

    private List<(int x, int y)> GetValidMoves((int x, int y) pos, char[,] grid, int rows, int cols)
    {
        var moves = new List<(int x, int y)>
        {
            (pos.x - 1, pos.y), // Up
            (pos.x + 1, pos.y), // Down
            (pos.x, pos.y - 1), // Left
            (pos.x, pos.y + 1)  // Right
        };

        return moves.Where(move =>
            move.x >= 0 && move.x < rows &&
            move.y >= 0 && move.y < cols &&
            grid[move.x, move.y] != 'X').ToList();
    }

    private List<int[]> BuildPath(Dictionary<(int x, int y), (int x, int y)?> parent, (int x, int y) goal)
    {
        var path = new List<(int x, int y)>();
        var current = goal;

        while (parent.TryGetValue(current, out var next))
        {
            path.Add(current);
            if (next == null)
                break;

            current = next.Value;
        }

        path.Reverse();

        return path.Select(p => new int[] { p.Item1, p.Item2 }).ToList();
    }
}
