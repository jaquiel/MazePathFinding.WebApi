using MazePathFinding.WebApi.Controllers;
using MazePathFinding.WebApi.Services;
using MazePathfindingAPI.Services;
using Moq;

namespace MazePathFinding.WebApi.Tests;

public class MazesControllerFixture : IDisposable
{
    public IMazesService _mazesService { get; private set; }
    public Mock<MazesService> _mazesServiceMock { get; private set; } = new Mock<MazesService>();

    public MazesController _mazesController { get; private set; }

    public MazesControllerFixture()
    {
        _mazesService = _mazesServiceMock.Object;
        _mazesController = new MazesController(_mazesService);
    }

    public void Dispose() { }
}
