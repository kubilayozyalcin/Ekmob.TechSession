using RabbitMQ.Client;
using System;

namespace Ekmob.TechSession.RabbitMQ
{
    // Connectionlar Disposable olmalıdır. Bu nedenle IDisposable interface inden türetilir.
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
