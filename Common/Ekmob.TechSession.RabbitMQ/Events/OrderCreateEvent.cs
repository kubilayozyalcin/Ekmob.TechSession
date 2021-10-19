using Ekmob.TechSession.Events.Interfaces;
using System;

namespace Ekmob.TechSession.Events
{
    public class OrderCreateEvent : IEvent
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
