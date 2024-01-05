using App.Models.Entity;
using RabbitMQ.Client;
using App.Repository.Interfaces;
using System.Text;
using System.Text.Json;

namespace App.Repository
{
    public class RabbitMessageRepository : IAppMessageRepository
    {
        public void SendMessage(AppMessage message)
        {
            var factory = new ConnectionFactory()
            { 
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: "rabbitMessageQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "rabbitMessageQueue",
                                     basicProperties: null,
                                     body: body);
            }
            
        }
    }
}
