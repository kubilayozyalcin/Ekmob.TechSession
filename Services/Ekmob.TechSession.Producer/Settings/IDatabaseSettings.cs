using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Settings
{
    public interface IDatabaseSettings
    {
        public string EmployeeCollectionName { get; set; }
        public string DepartmentCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
