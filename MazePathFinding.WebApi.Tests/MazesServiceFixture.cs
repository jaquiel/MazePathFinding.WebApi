using MazePathFinding.WebApi.Services;
using MazePathfindingAPI.Services;
using Moq;

namespace MazePathFinding.WebApi.Tests;

public class MazesServiceFixture : IDisposable
{ 
    public IMazesService _mazesService { get; private set; }
    public Mock<MazesService> MazesServiceMock { get; private set; } = new Mock<MazesService>();

    public MazesServiceFixture() => _mazesService = MazesServiceMock.Object;
    
    public void Dispose() { }
}
