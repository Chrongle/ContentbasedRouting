using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    string exchangeName = "AnimalClinicRouter";

    SendMessage(channel, exchangeName, "tiger", "Very hungry tiger in need of food");
    SendMessage(channel, exchangeName, "zebra", "Zebra bitten on the butt");

    Console.WriteLine(" [*] Messages sent for tiger and zebra treatments.");
}

void SendMessage(IModel channel, string exchangeName, string animalType, string message)
{
    var body = Encoding.UTF8.GetBytes(message);
    var properties = channel.CreateBasicProperties();
    properties.Headers = new Dictionary<string, object> { { "animalType", animalType } };

    channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: properties, body: body);
    Console.WriteLine($" [x] Sent '{message}' with animalType '{animalType}'");
}