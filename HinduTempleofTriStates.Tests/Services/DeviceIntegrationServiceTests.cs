using Moq;
using Xunit;
using HinduTempleofTriStates.Services;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class DeviceIntegrationServiceTests
{
    private readonly Mock<IDeviceInteractionRepository> _mockRepo;
    private readonly Mock<IDonationService> _mockDonationService; // Add mock for IDonationService
    private readonly Mock<ILogger<DeviceIntegrationService>> _mockLogger;
    private readonly DeviceIntegrationService _service;

    public DeviceIntegrationServiceTests()
    {
        _mockRepo = new Mock<IDeviceInteractionRepository>();
        _mockDonationService = new Mock<IDonationService>(); // Instantiate the donation service mock
        _mockLogger = new Mock<ILogger<DeviceIntegrationService>>();

        // Pass all required mocks into the DeviceIntegrationService constructor
        _service = new DeviceIntegrationService(_mockRepo.Object, _mockDonationService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task HandleDonationFromDeviceAsync_ValidDonation_ShouldProcessSuccessfully()
    {
        // Arrange
        var donation = new Donation { Id = Guid.NewGuid(), DonorName = "Test Donor", Amount = 100 };

        // Act
        await _service.HandleDonationFromDeviceAsync(donation, "Device1");

        // Assert
        _mockRepo.Verify(repo => repo.AddInteractionAsync(It.IsAny<DeviceInteraction>()), Times.Once);
    }
}
