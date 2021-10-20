using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Ekmob.TechSession.RabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly int _retryCount;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private bool _disposed;

        public DefaultRabbitMQPersistentConnection(
            IConnectionFactory connectionFactory, 
            int retryCount, 
            ILogger<DefaultRabbitMQPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _retryCount = retryCount;
            _logger = logger;
        }

        public bool IsConnected {
            get
            {
                // Connecttion Not Null & Open & Not Disposed
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect");

            // Polly frameworkü ile bir retryPolicy oluşturularak tekrar tekrar bağlantı sağlanmaya çalışılır.
            // SocketException yada BrokerUnreachableException rabbitMQ ye ulaşamadığı durumlarda hata fırlatır : RabbitMQ kütüphanesi
            // Hata alındığında WaitAndRetry adında Belirlenen koşullarda Bekle terkrar dene policy oluşturulur.
            // retryAttempt kaçıncı deneme olduğunu gösterir deneme sayısına göre bekleme süresi saniye olarak hesaplanarak 
            // tekrar bağlanma isteği oluşturulur.

            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", 
                        $"{time.TotalSeconds:n1}", ex.Message);
                });

            // Execute metodu run edilerek connection oluşturulur.
            policy.Execute(() => {
                _connection = _connectionFactory.CreateConnection();
            });

            // İşlemler başarılı değilse ilgili eventlere yönlendirilir. Bu metodlarda tekrar connect olmasını sağlıyor olacağız.
            if(IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' " +
                    "and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }

        // Try connect IModel tipinde bir nesne geriye döner. Burada Q management işlemlerinin yapıldığı metodlar bulunuyor.
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        // Why Use Dispose : Connection'lar uygulamalar üzerinde yönetilmesi gereken yükler oluşturur bu neden bu sınıflar 
        // IDisposable interface i ile oluşturulur.
        public void Dispose()
        {
            // If Object Disposed Return
            if (_disposed) return;

            // Else Dispose Status = true and connection try dispose
            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    }
}
