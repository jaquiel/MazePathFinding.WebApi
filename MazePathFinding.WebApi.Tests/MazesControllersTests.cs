using MazePathFinding.WebApi.Models;
using MazePathfindingAPI.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MazePathFinding.WebApi.Tests;

public class MazesControllersTests : IClassFixture<MazesControllerFixture>
{
    private readonly MazesControllerFixture _fixture;

    public MazesControllersTests(MazesControllerFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GivenAValidGrid_WhenTrySubmitMaze_ReturnsOkResult()
    {
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>>
                {
                    new List<char> { 'S', '_', '_', '_', '_' },
                    new List<char> { 'X', 'X', 'X', 'X', '_' },
                    new List<char> { '_', '_', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', '_', 'G' }
                }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ solution = S  -> [0,0] -> [0,1] -> [0,2] -> [0,3] -> [0,4] -> [1,4] -> [2,4] -> [3,4] -> [4,4] -> G }";
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actual = Assert.IsType<string>(okResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAGridWithoutGoalPoint_WhenTrySubmitMaze_ReturnsBadRequestResult()
    {
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>>
                {
                    new List<char> { 'S', '_', '_', '_', '_' },
                    new List<char> { 'X', 'X', 'X', 'X', '_' },
                    new List<char> { '_', '_', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', '_', 'X' }
                }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ error = Maze must contain one start point (S) and one goal point (G). }";
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var actual = Assert.IsType<string>(badRequestResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAGridWithoutValidPath_WhenTrySubmitMaze_ReturnsOkWithNoPathSolution()
    {
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>>
                {
                    new List<char> { 'S', '_', '_', '_', '_' },
                    new List<char> { 'X', 'X', 'X', 'X', '_' },
                    new List<char> { '_', '_', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', 'X' },
                    new List<char> { '_', 'X', '_', '_', 'G' }
                }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ solution = No solution found for the maze. }";
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actual = Assert.IsType<string>(okResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAnInvalidGrid_WhenTrySubmitMaze_ReturnsBadRequestResult()
    {
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>> { }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ error = Invalid maze grid. }";
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var actual = Assert.IsType<string>(badRequestResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAGridWithMaxRowsExceeded_WhenTrySubmitMaze_ReturnsBadRequestResult()
    {
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>>
                {
                    new List<char> { 'S', '_', '_', '_', '_' },
                    new List<char> { 'X', 'X', 'X', 'X', '_' },
                    new List<char> { '_', '_', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', '_', 'G' }
                }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ error = Maze exceeds maximum size of 20 rows x 20 columns. }";
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var actual = Assert.IsType<string>(badRequestResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAGridWithDifferentNumberOfColumns_WhenTrySubmitMaze_ReturnsBadRequestResult()
    {          
        // Arrange
        var request = new MazeRequest
        {
            Grid = new List<List<char>>
                {
                    new List<char> { 'S', '_', '_', '_', '_' },
                    new List<char> { '_', '_', '_', '_' },
                    new List<char> { '_', '_', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', 'X', '_' },
                    new List<char> { '_', 'X', '_', '_', 'G' }
                }
        };

        // Act
        var result = _fixture._mazesController.SubmitMaze(request);

        // Assert
        var expected = "{ error = Maze must have the same number of columns in each row. }";
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var actual = Assert.IsType<string>(badRequestResult?.Value?.ToString());
        Assert.Equal(expected, actual);
    }
}