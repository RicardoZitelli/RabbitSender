﻿using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";

IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey, arguments: null);

//Encode the message
byte[] messageBodyBytes = Encoding.UTF8.GetBytes("Hello World");

//Publish the message
channel.BasicPublish(exchangeName,routingKey,basicProperties:null, messageBodyBytes);

channel.Close();
cnn.Close();