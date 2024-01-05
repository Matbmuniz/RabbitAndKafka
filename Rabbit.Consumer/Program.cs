using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using App.Models.Entity;
using Confluent.Kafka;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//RabbitMQ
//var factory = new ConnectionFactory
//{
//    HostName = "localhost",
//    UserName = "admin",
//    Password = "123456"
//};
//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();
//{
//    channel.QueueDeclare(queue: "rabbitMessageQueue",
//                         durable: false,
//                         exclusive: false,
//                         autoDelete: false,
//                         arguments: null);

//    var consumer = new EventingBasicConsumer(channel);
//    consumer.Received += (model, ea) =>
//    {
//        var body = ea.Body.ToArray();
//        var json = Encoding.UTF8.GetString(body);

//        RabbitMessage mensagem = JsonSerializer.Deserialize<RabbitMessage>(json);

//        Thread.Sleep(1000);

//        Console.WriteLine($" Titulo: {mensagem.Titulo}, Texto: {mensagem.Texto}; Id: {mensagem.Id}");
//    };
//    channel.BasicConsume(queue: "rabbitMessageQueue",
//                         autoAck: true,
//                         consumer: consumer);

//    Console.WriteLine(" Press [enter] to exit.");
//    Console.ReadLine();
//}


//Kafka
CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = $"queueKafka-group-0",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe("queueKafka");
    while (!cts.IsCancellationRequested)
    {
        try
        {
            var cr = consumer.Consume(cts.Token);
            var json = cr.Message.Value;
            AppMessage mensagem = JsonSerializer.Deserialize<AppMessage> (json);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"Titulo: {mensagem.Titulo}; Texto={mensagem.Texto}; Id={mensagem.Id}");
        }
        catch (OperationCanceledException oce)
        {
            continue;
        }
    }
}
