using Ekmob.TechSession.Events.Abstractions;
using Ekmob.TechSession.RabbitMQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;

namespace Ekmob.TechSession.Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQProducer> _logger;
        private readonly int _retryCount;

        public EventBusRabbitMQProducer(IRabbitMQPersistentConnection persistentConnection, 
            ILogger<EventBusRabbitMQProducer> logger, int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _retryCount = retryCount;
        }

        // @ işareti : event anlamlı bir keyword olduğu için @event
        public void Publish(string queueName, IEvent @event)
        {
            // check connection
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            // Policy üzerinden execute işlemi yapılır.
            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", 
                    @event.RequestId, $"{time.TotalSeconds:n1}", ex.Message);
            });

            
            using(var channel = _persistentConnection.CreateModel())
            {
                // Channel üzerinden Q Declare edilir. Mesaj Q ye hazır hale getirilir ve publish işlemi yapılır. 
                // Hata alındığında retryPolicy üzerinden tekrar tekrar deneme yapılır.
                // https://rabbitmq.com/queues.html parametreler.
                // queueName : Q Name
                // durable : eğer false ise inmemory olarak tutar. true ise rabbitMQ sunucusunda tutar ve restart sonrası silinmez
                // exclusive : Q nun tek bir connection'a sahip olur. 1 tane Consumer burayı connect edebilir. Consumer silinince connection
                // kapandığında Q silinir. 
                // autoDelete : Q en az 1 consumer a sahip ise q otomatik olarak silinir.

                channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                // Json formatta Q ya mesaj bırakılır.
                var message = JsonConvert.SerializeObject(@event);
                // Byte Listesi haline dönüştür. 
                var body = Encoding.UTF8.GetBytes(message);

                // Mikroservice güvenirliğini arttırmak için Polly Kütüphanesi kullanılır. 
                policy.Execute(() =>
                {
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.DeliveryMode = 2;

                    channel.ConfirmSelect();
                    channel.BasicPublish(
                        exchange: "", // default
                        routingKey: queueName, // q name
                        mandatory: true,
                        basicProperties: properties, // basic properties
                        body: body);  // message
                    channel.WaitForConfirmsOrDie();

                    channel.BasicAcks += (sender, eventArgs) =>
                    {
                        Console.WriteLine("Sent RabbitMQ");
                    };
                });
            }
        }
    }
}
