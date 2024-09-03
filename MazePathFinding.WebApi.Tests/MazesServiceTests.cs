using MazePathFinding.WebApi.Services;
using MazePathfindingAPI.WebApi.Models;

namespace MazePathFinding.WebApi.Tests;

public class MazesServiceTests : IClassFixture<MazesServiceFixture>
{
    private readonly IMazesService _mazesService;

    public MazesServiceTests(MazesServiceFixture fixture)
    {
        _mazesService = fixture._mazesService;
    }

    [Fact]
    public void GivenAValidMaze_WhenTryToSolve_ReturnsCorrectPath()
    {
        var grid = new char[,]
        {
            { 'S', '_', '_', '_', '_' },
            { 'X', 'X', 'X', 'X', '_' },
            { '_', '_', '_', 'X', '_' },
            { '_', 'X', '_', 'X', '_' },
            { '_', 'X', '_', '_', 'G' }
        }; 

        var maze = new Maze
        {
            Grid = grid,
            Start = (0,0),
            Goal = (4,4)
        };

        var expectedPath = new List<int[]>
        {
            new int[] {0, 0},
            new int[] {0, 1},
            new int[] {0, 2},
            new int[] {0, 3},
            new int[] {0, 4},
            new int[] {1, 4},
            new int[] {2, 4},
            new int[] {3, 4},
            new int[] {4, 4},
        };

        maze.Solution = _mazesService.SolveMaze(maze);

        Assert.Equal(expectedPath, maze.Solution);
    }

    [Fact]
    public void GivenAInvalidMaze_WhenTryToSolve_ReturnsNull()
    {
           var grid = new char[,]
           {
            { 'S', '_', '_', '_', '_' },
            { 'X', 'X', 'X', 'X', '_' },
            { '_', '_', '_', 'X', '_' },
            { '_', 'X', '_', 'X', '_' },
            { '_', 'X', '_', '_', 'X' }
        };

        var maze = new Maze
        {
            Grid = grid,
            Start = (0,0),
            Goal = (4,4)
        };

        maze.Solution = _mazesService.SolveMaze(maze);

        Assert.Null(maze.Solution);
    }
}