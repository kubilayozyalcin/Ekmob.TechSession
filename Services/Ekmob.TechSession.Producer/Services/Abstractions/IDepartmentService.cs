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
        Task<Response<IEnumerable<Department>>> GetDepartments();
        Task<Response<Department>> GetDepartment(string id);
        Task<Response<Department>> Create(Department department);
        Task<Response<NoContent>> Update(Department department);
        Task<Response<NoContent>> Delete(string id);
    }
}
