using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDbContext companyDb, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            DepartmentRepository = departmentRepository;
            EmployeeRepository = employeeRepository;
            _context = companyDb;

        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
           return _context.SaveChanges();
        }
    }
}
