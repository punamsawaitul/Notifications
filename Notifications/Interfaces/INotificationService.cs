﻿using Notifications.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Interfaces
{
    public interface INotificationService
    {
        NotificationResultModel SendNotification(MailRequest mailRequest);
    }
}
