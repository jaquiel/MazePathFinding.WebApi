using MazePathfindingAPI.WebApi.Models;

namespace MazePathFinding.WebApi.Services;

public interface IMazesService
{
    List<int[]> SolveMaze(Maze maze);
}
