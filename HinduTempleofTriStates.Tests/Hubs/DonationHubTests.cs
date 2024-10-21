using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;
using System.Threading.Tasks;
using HinduTempleofTriStates.Hubs;

public class DonationHubTests
{
    private readonly Mock<IHubCallerClients> _mockClients;
    private readonly Mock<IClientProxy> _mockClientProxy;
    private readonly DonationHub _hub;

    public DonationHubTests()
    {
        _mockClients = new Mock<IHubCallerClients>(); // Use IHubCallerClients instead of IHubClients
        _mockClientProxy = new Mock<IClientProxy>();
        _mockClients.Setup(clients => clients.All).Returns(_mockClientProxy.Object);
        _hub = new DonationHub { Clients = _mockClients.Object }; // Assign the mock to the Clients property
    }

    [Fact]
    public async Task SendDonationUpdate_ShouldSendUpdateToAllClients()
    {
        // Act
        await _hub.SendDonationUpdate("123", "Donation Received");

        // Assert
        _mockClientProxy.Verify(client => client.SendCoreAsync("ReceiveDonationUpdate",
            It.Is<object[]>(o => (string)o[1] == "Donation Received"), default), Times.Once);
    }
}
