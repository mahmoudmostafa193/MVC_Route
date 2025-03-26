using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_EmployeeRepository.GetAll());
        }


        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = _EmployeeRepository.Get(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(viewname, model);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return BadRequest();
            //}
            //var model = _departmentRepository.Get(id.Value);
            //if (model == null)
            //{
            //    return NotFound();
            //}
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var emp = new Employee
                {
                   Name = model.Name,
                   Age=model.Age, 
                   Email=model.Email,
                   
                   Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate

                };
                var c = _EmployeeRepository.Update(emp);

                if (c > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var EMP = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate
                };
                var count = _EmployeeRepository.Add(EMP);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);

                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return BadRequest();
            //}
            //var model = _departmentRepository.Get(id.Value);
            //if (model == null)
            //{
            //    return NotFound();
            //}
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var c = _EmployeeRepository.Delete(model);
            if (c > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }



    }
}
