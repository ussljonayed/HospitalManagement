using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using HospitalManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HospitalManagement.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        public readonly _DbContext _context;

        public DoctorDepartmentController(_DbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int pageSize = 5, string search = "")
        {
            var doctordepartments = _context.DoctorDepartments;

            Pagination p = new Pagination(doctordepartments.Count(), page, pageSize);
            ViewBag.Pagination = p;
            var result = doctordepartments.Skip(p.Skip).Take(pageSize).ToList<DoctorDepartment>();

            return View(result);
        }


        [ActionName("Create")]
        public IActionResult Save()
        {
            return View(_context.DoctorDepartments);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name")] DoctorDepartment doctordepartment)
        {


            _context.DoctorDepartments.Add(doctordepartment);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var u = _context.DoctorDepartments.Include(u => u.Doctors).Where(u => u.Id == id).FirstOrDefault();
            return View(u);
        }

        public IActionResult Edit(int id)
        {

            var dd = _context.DoctorDepartments.Find(id);

            return View(dd);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id", "Name")] DoctorDepartment doctordepartment)
        {

            if (Id != doctordepartment.Id)
            {
                return NotFound();
            }

            _context.Update(doctordepartment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            
            var u = _context.DoctorDepartments.Where(u => u.Id == id).SingleOrDefault();
            


            return View(u);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var doctordepartment = _context.DoctorDepartments.Find(id);
            _context.Remove(doctordepartment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
