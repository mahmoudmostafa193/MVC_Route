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


        [HttpGet]
        public IActionResult Details(int? id, string viewname= "Details")
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = _departmentRepository.Get(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(viewname,model);
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
            return Details(id,"Edit");
        }
        [HttpPost]
        public IActionResult Edit(CreateDepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var department = new Department
                {
                    Code = model.Code,
                    Name = model.Name,

                };
                var c = _departmentRepository.Update(department);

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
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete(Department model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var c = _departmentRepository.Delete(model);
            if (c > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }



    }

}
