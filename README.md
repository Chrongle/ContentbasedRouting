Contentbased Routing Messaging animal clinic example for School Project.

Prerequisites:
  - RabbitMQ has to be running

What does it do?
  In this example a Producer sends 2 messages to a Router.
  The Router configures the queues and routes them by headers.
  
How to use:
  - Run the Router first.
  - Run all of the Consumers.
  - While the Consumers are running, run the Producer. The producer will send 2 messages routed to the Consumers. All of the messages are shown in the consoles.
