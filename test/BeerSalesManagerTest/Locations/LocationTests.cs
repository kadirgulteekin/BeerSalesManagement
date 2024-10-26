using Application.Abstractions.Data;
using Application.Location.AddLocation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BeerSalesManagerTest.Locations;

public class LocationTests : BaseTest
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly AddLocationCommandHandler _handler;

    public LocationTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _handler = new AddLocationCommandHandler(_mockContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddLocation_WhenValidCommand()
    {
        // Arrange
        var command = new AddLocationCommand
        {
            LocationName = "Test Location"
        };
     
        var locationsDbSetMock = new Mock<DbSet<Domain.Beers.Location>>();
        _mockContext.Setup(c => c.Locations).Returns(locationsDbSetMock.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        locationsDbSetMock.Verify(l => l.Add(It.Is<Domain.Beers.Location>(loc => loc.Name == command.LocationName)), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}
