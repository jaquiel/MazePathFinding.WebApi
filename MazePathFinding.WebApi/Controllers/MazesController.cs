using MazePathFinding.WebApi.Extensions;
using MazePathFinding.WebApi.Helpers;
using MazePathFinding.WebApi.Models;
using MazePathFinding.WebApi.Services;
using MazePathfindingAPI.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MazePathFinding.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MazesController : ControllerBase
{
    private readonly IMazesService _mazesService;
    private static readonly List<Maze> _mazes = new List<Maze>();

    public MazesController(IMazesService mazesService)
    {
        _mazesService = mazesService;
    }

    /// <summary>
    /// Submits a maze to be solved.
    /// </summary>
    /// <param name="mazeRequest"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/mazes
    ///     {
    ///         "grid": [
    ///             ["S", "_", "_", "_", "_"],
    ///             ["X", "X", "X", "X", "_"],
    ///             ["_", "_", "_", "X", "_"],
    ///             ["_", "X", "_", "X", "_"],
    ///             ["_", "X", "_", "_", "G"]
    ///         ]
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<int[]>), 200)]
    [ProducesResponseType(typeof(List<int[]>), 400)]
    public ActionResult<List<int[]>> SubmitMaze([FromBody] MazeRequest mazeRequest)
    {
        var requestValidation = mazeRequest.Validate();

        if (!requestValidation.Equals(string.Empty))
        {
            return BadRequest(requestValidation.Error());
        }

        var gridArray = Helper.ConvertArrayFromJaggedToMultidimensional(mazeRequest.Grid);

        var start = Helper.FindPoint(gridArray, 'S');
        var goal = Helper.FindPoint(gridArray, 'G');

        if (start == null || goal == null)
        {
            return BadRequest("Maze must contain one start point (S) and one goal point (G).".Error());
        }

        var maze = new Maze
        {
            Grid = gridArray,
            Start = start.Value,
            Goal = goal.Value
        };

        maze.Solution = _mazesService.SolveMaze(maze);

        _mazes.Add(maze);

        return maze.Solution != null ? Ok(Helper.GetFormattedAsString(maze.Solution).Success())
                                     : Ok("No solution found for the maze.".Success());
    }

    /// <summary>
    /// Gets all mazes submitted.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/mazes { } 
    /// 
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult GetMazes() => _mazes.Count != 0 ? Ok(_mazes) : NotFound("Not found any maze");
}
