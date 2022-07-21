using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;

namespace Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
        private readonly INotificationService _notificationService;
    }
}
