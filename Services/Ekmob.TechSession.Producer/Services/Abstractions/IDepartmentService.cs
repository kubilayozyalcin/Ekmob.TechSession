using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Shared.Utilities.Response;
using System.Collections.Generic;
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
