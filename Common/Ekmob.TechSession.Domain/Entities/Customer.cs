using Ekmob.TechSession.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ekmob.TechSession.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string CreateDate { get; set; }
    }
}
