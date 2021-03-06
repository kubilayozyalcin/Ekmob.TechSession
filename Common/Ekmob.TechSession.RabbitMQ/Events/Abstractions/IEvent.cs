using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Events.Abstractions
{
    public abstract class IEvent
    {
        // Sadece bu metod içinde set edilecek "private init"
        public Guid RequestId { get; private init; }
        public DateTime CreationDate { get; private init; }

        public IEvent()
        {
            RequestId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
