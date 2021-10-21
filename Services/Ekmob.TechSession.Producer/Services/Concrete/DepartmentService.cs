using AutoMapper;
using Ekmob.TechSession.Shared.Utilities.Response;
using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ekmob.TechSession.Producer.Dtos;

namespace Ekmob.TechSession.Producer.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ISourcingContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(ISourcingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<DepartmentDto>> GetDepartment(string id)
        {
            var department = await _context.Departments.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (department == null)
                return Response<DepartmentDto>.Fail("Department not found", 404);

            return Response<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(department), 200);
        }

        public async Task<Response<List<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments.Find(department => true).ToListAsync();

            return Response<List<DepartmentDto>>.Success(_mapper.Map<List<DepartmentDto>>(departments), 200);
        }
        
        public async Task<Response<DepartmentDto>> Create(DepartmentCreateDto department)
        {
            var newDepartment = _mapper.Map<Department>(department);
            await _context.Departments.InsertOneAsync(newDepartment);
            return Response<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(department), 200);
        }

    }
}
