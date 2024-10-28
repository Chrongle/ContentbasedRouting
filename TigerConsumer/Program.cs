using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "TigerQueue", durable: false, exclusive: false, autoDelete: false);
    
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [TigerConsumer] Treatment for tiger received: {message}");
    };

    channel.BasicConsume(queue: "TigerQueue", autoAck: true, consumer: consumer);

    Console.WriteLine(" [*] Waiting for tiger treatments...");
    Console.ReadLine();
}