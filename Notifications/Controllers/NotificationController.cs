using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;
using Notifications.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notifications.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //// GET: api/<NotificationController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<NotificationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST 
        [Route("SendNotification")]
        [HttpPost]
        public async Task<IActionResult> Post(MailRequest mailRequest)
        {
            try
            {
                NotificationResultModel responseData = _notificationService.SendNotification(notificationModel);

                if (responseData.Result)
                {
                    responseData.Message = "Notification sent successfully!";
                }
                else
                {
                    responseData.Message = "Sending notification failed!";
                }

                return await Task.FromResult(Ok(responseData));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(ex.Message));
            }
        }

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
