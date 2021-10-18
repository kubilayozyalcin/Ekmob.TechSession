using Ekmob.TechSession.Core.Utilities.Response;
using Ekmob.TechSession.Producer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Services.Abstractions
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(string id);

        Task Create(Department department);
        Task<bool> Update(Department department);
        Task<bool> Delete(string id);
    }
}
