using AutoMapper;
using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository EmployeeRepository,IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _DepartmentRepository = departmentRepository;
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            if(SearchInput!=null)
            {
                return View(_EmployeeRepository.GetByName(SearchInput));
            }
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
            if (id == null)
            {
                return BadRequest();
            }

            var employee = _EmployeeRepository.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var departments = _DepartmentRepository.GetAll()
                .Select(d => new DepartmentDTO { Id = d.Id, Name = d.Name })
                .ToList();

            var model = new CreateEmployeeDTO
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Address = employee.Address,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.HiringDate,
                DepartmentId = employee.DepartmentId,
                Departments = departments
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreateEmployeeDTO model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _DepartmentRepository.GetAll()
                    .Select(d => new DepartmentDTO { Id = d.Id, Name = d.Name })
                    .ToList();
                return View(model);
            }

            var emp = new Employee
            {
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Salary = model.Salary,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                HiringDate = model.HiringDate,
                DepartmentId = model.DepartmentId
            };

            var c = _EmployeeRepository.Update(emp);

            if (c > 0)
            {
                return RedirectToAction("Index");
            }

            model.Departments = _DepartmentRepository.GetAll()
                .Select(d => new DepartmentDTO { Id = d.Id, Name = d.Name })
                .ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _DepartmentRepository.GetAll()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            ViewBag.Departments = departments;

            var model = new CreateEmployeeDTO();
            return View(model);
        }



        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _DepartmentRepository.GetAll()
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();

                return View(model);
            }

           var EMP=_mapper.Map<Employee>(model);

            var count = _EmployeeRepository.Add(EMP);

            if (count > 0)
            {
                ViewBag.Message = "✅ Employee Added Successfully!";
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            ViewBag.Error = "❌ Failed to add Employee. Please try again.";
            ViewBag.Departments = _DepartmentRepository.GetAll()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

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
