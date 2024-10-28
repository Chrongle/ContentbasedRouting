using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "ZebraQueue", durable: false, exclusive: false, autoDelete: false);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [ZebraConsumer] Treatment for zebra received: {message}");
    };

    channel.BasicConsume(queue: "ZebraQueue", autoAck: true, consumer: consumer);

    Console.WriteLine(" [*] Waiting for zebra treatments...");
    Console.ReadLine();
}