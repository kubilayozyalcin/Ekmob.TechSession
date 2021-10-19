using AutoMapper;
using Ekmob.TechSession.Core.Utilities.Response;
using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Response<Department>> GetDepartment(string id)
        {
            var department = await _context.Departments.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (department == null)
                return Response<Department>.Fail("Department not found", 404);

            return Response<Department>.Success(_mapper.Map<Department>(department), 200);
        }

        public async Task<Response<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _context.Departments.Find(department => true).ToListAsync();

            return Response<IEnumerable<Department>>.Success(_mapper.Map<IEnumerable<Department>>(departments), 200);
        }

        public async Task<Response<Department>> Create(Department department)
        {
            var newDepartment = _mapper.Map<Department>(department);
            await _context.Departments.InsertOneAsync(department);
            return Response<Department>.Success(_mapper.Map<Department>(department), 200);
        }

        public async Task<Response<NoContent>> Update(Department department)
        {
            var updateDepartment = _mapper.Map<Department>(department);

            var result = await _context.Departments.FindOneAndReplaceAsync(x => x.Id == updateDepartment.Id, department);

            if (result == null)
                return Response<NoContent>.Fail("Department not found", 404);

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> Delete(string id)
        {
            var result = await _context.Departments.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
                return Response<NoContent>.Success(204);
            else
                return Response<NoContent>.Fail("Employee not found", 404);
        }
    }
}
