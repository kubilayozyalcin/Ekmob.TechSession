using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Shared.Messages
{
    public class CreateEmployeeCommand
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
