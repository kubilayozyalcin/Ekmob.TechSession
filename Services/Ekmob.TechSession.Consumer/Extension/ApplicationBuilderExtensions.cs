using Ekmob.TechSession.Consumer.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ekmob.TechSession.Consumer.Extension
{
    public static class ApplicationBuilderExtensions
    {
        // Erişim Sağlanır
        public static EventBusCustomerCreateConsumer Listener { get; set; }

        // Uygulama Ayağa kaldırılırken uygulamanın life time ında bu extension üzerinden consumer ı başlatıp durdurmak.
        public static IApplicationBuilder UseEventBusListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusCustomerCreateConsumer>();
            var lifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifeTime.ApplicationStarted.Register(OnStarted);
            lifeTime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
