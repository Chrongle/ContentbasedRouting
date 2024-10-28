using System;
using System.Collections.Generic;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "AnimalClinicRouter", type: ExchangeType.Headers);

    channel.QueueDeclare("TigerQueue", durable: false, exclusive: false, autoDelete: false);
    channel.QueueDeclare("ZebraQueue", durable: false, exclusive: false, autoDelete: false);

    channel.QueueBind(queue: "TigerQueue", exchange: "AnimalClinicRouter", routingKey: "", 
                      arguments: new Dictionary<string, object> { { "animalType", "tiger" } });
    channel.QueueBind(queue: "ZebraQueue", exchange: "AnimalClinicRouter", routingKey: "", 
                      arguments: new Dictionary<string, object> { { "animalType", "zebra" } });

    Console.WriteLine(" [*] AnimalClinicRouter configured with queues for tigers and zebras.");
    Console.ReadLine();
}