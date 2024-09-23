using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DeviceIntegrationController : Controller 
    {
        private readonly IDeviceIntegrationService _deviceIntegrationService;
        private readonly ILogger<DeviceIntegrationController> _logger;

        public DeviceIntegrationController(IDeviceIntegrationService deviceIntegrationService, ILogger<DeviceIntegrationController> logger)
        {
            _deviceIntegrationService = deviceIntegrationService ?? throw new ArgumentNullException(nameof(deviceIntegrationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Endpoint to handle donations from handheld devices
        [HttpPost("donation")]
        public async Task<IActionResult> CreateDonationFromDevice([FromBody] Donation donation, [FromQuery] string deviceName)
        {
            if (donation == null)
            {
                _logger.LogWarning("Received null donation data from device {DeviceName}.", deviceName);
                return BadRequest("Donation data is required.");
            }

            try
            {
                await _deviceIntegrationService.HandleDonationFromDeviceAsync(donation, deviceName);
                return Ok("Donation successfully processed from device.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing donation from device {DeviceName}.", deviceName);
                return StatusCode(500, "Error processing donation.");
            }
        }

        // Endpoint to get all device interactions
        [HttpGet("interactions")]
        public async Task<IActionResult> GetAllInteractions()
        {
            var interactions = await _deviceIntegrationService.GetAllInteractionsAsync();
            return Ok(interactions);
        }
        // Action to render the Booking Status page for Handheld Integration
        [HttpGet]
        public IActionResult RealTimeUpdates()
        {
            return View(); // Renders the view named RealTimeUpdates.cshtml
        }

        [HttpGet]
        public IActionResult BookingStatus()
        {
            return View(); // Renders the view named BookingStatus.cshtml
        }
    }
}
