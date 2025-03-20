using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;
        public DepartmentRepository(CompanyDbContext context)
        {
            this._context = context;
        }
        public int Add(Department model)
        {
          
            _context.Departments.Add(model);
            return _context.SaveChanges();
            
        }

        public int Delete(Department model)
        {
           
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

        public Department? Get(int id)
        {
     

            return _context.Departments.FirstOrDefault(d=> d.Id==id);
        }

        public IEnumerable<Department> GetAll()
        {
            

            return _context.Departments.ToList();

        
        }

        public int Update(Department model)
        {
           
            _context.Departments.Update(model);
            return _context.SaveChanges();

        }
    }
}
