using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ekmob.TechSession.Domain.Entities.Base
{
    public abstract class Entity : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; protected set; }

        // For Clone Entity Model
        public Entity Clone()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
