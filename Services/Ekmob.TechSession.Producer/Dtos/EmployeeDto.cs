namespace Ekmob.TechSession.Producer.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitle { get; set; }

        public DepartmentDto Department { get; set; }

    }
}
