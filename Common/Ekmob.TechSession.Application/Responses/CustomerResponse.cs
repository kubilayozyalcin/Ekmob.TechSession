using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Application.Responses
{
    public class CustomerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string CreateDate { get; set; }
    }
}
