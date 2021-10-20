using Ekmob.TechSession.Producer.Dtos;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Shared.Utilities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Services.Abstractions
{
    public interface IDepartmentService
    {
        Task<Response<List<DepartmentDto>>> GetDepartments();
        Task<Response<DepartmentDto>> GetDepartment(string id);
        Task<Response<DepartmentDto>> Create(DepartmentCreateDto department);
    }
}
