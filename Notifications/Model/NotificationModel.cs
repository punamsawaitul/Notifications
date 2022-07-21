using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Model
{
    public class NotificationModel
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public int UserPhoneNumber { get; set; }
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }       
    }
}
