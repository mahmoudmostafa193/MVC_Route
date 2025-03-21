using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController:Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_departmentRepository.GetAll());
        }
        //[HttpGet("{id}")]
        //public IActionResult Details(int id)
        //{
        //    var model = _departmentRepository.Get(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var Department = new Department()
                {
                   
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count= _departmentRepository.Add(Department);
                if(count>0)
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
    }

}
