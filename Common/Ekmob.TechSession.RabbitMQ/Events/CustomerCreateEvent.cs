using Ekmob.TechSession.Events.Abstractions;
using System;

namespace Ekmob.TechSession.RabbitMQ.Events
{
    public class CustomerCreateEvent : IEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
    }
}
