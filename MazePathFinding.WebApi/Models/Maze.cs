namespace MazePathfindingAPI.WebApi.Models;

public record Maze
{
    public char[,] Grid { get; set; }
    public (int x, int y) Start { get; set; }
    public (int x, int y) Goal { get; set; }
    public List<int[]> Solution { get; set; }
}
