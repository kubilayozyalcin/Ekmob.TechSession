using AutoMapper;
using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.RabbitMQ;
using Ekmob.TechSession.RabbitMQ.Core;
using Ekmob.TechSession.RabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Ekmob.TechSession.Consumer.Consumers
{
    public class EventBusCustomerCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusCustomerCreateConsumer(IRabbitMQPersistentConnection rabbitMQPersistentConnection, 
            IMediator mediator, IMapper mapper)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection;
            _mediator = mediator;
            _mapper = mapper;
        }

        public void Consume()
        {
            if(!_rabbitMQPersistentConnection.IsConnected == false)
            {
                _rabbitMQPersistentConnection.TryConnect();
            }

            // Q Yönetecek Property
            var channel = _rabbitMQPersistentConnection.CreateModel();
            // Q yü Declare ediyoruz
            channel.QueueDeclare(queue: EventBusConstants.CustomerCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Q yü Cosumer etmeye yarayan nesne
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += RecivedEvent;

            // Consume edilecek QName ve Consumer verilerek Mesaj consume edilir.
            channel.BasicConsume(queue: EventBusConstants.CustomerCreateQueue, consumer: consumer);
        }

        // Gelen mesaj buraya düşer ve gelen mesaj için yapılacak işlemler
        private async void RecivedEvent(object sender, BasicDeliverEventArgs e)
        {
            // e.Body.Span ile mesaja ulaşılır
            var message = Encoding.UTF8.GetString(e.Body.Span);

            // Gönderilen Mesajı deserialize ederek kullanıma hazır hale getiriyoruz.
            var @event = JsonConvert.DeserializeObject<CustomerCreateEvent>(message);

            if(e.RoutingKey == EventBusConstants.CustomerCreateQueue)
            {
                var command = _mapper.Map<CustomerCreateCommand>(@event);
                command.CreateDate = DateTime.Now.ToString();
                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _rabbitMQPersistentConnection.Dispose();
        }
    }
}
