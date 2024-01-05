using App.Models.Entity;
using App.Repository.Interfaces;
using Confluent.Kafka;
using System.Text.Json;

namespace App.Repository
{
    public class KafkaMessageRepository : IAppMessageRepository
    {
        public void SendMessage(AppMessage message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                string json = JsonSerializer.Serialize(message);
                producer.Produce("queueKafka",
                                       new Message<string, string>
                                       { 
                                           Key = Guid.NewGuid().ToString(),
                                           Value = json
                                       });

            }
        }
    }
}
