using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class RabbitMQSettings
    {
        public RabbitMQSettings(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;

        }
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
