using Company.G02.DAL.Models;

namespace Company.G02.PL.DTO
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public int? DepartmentId { get; set; }
        public List<DepartmentDTO> Departments { get; set; } = new List<DepartmentDTO>();
    }
}
