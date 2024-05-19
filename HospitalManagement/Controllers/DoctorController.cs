using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using HospitalManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net;

namespace HospitalManagement.Controllers
{
    public class DoctorController : Controller
    {
        public readonly _DbContext _context;

        public DoctorController(_DbContext context)
        {
            _context = context;

        }
        //public IActionResult Index()
        //{
        //    var doctors = _context.Doctors.Include(u => u.DoctorDepartment);
        //    return Ok(doctors);
        //}


        public IActionResult Index(int page = 1, int pageSize = 5, string search = "")
        {
            var doctors = _context.Doctors.Include(u=> u.DoctorDepartment);

            Pagination p = new Pagination(doctors.Count(), page, pageSize);
            ViewBag.Pagination = p;
            var result = doctors.Skip(p.Skip).Take(pageSize).ToList<Doctor>();

            return View(result);
        }


        [ActionName("Create")]

        public IActionResult Save()
        {
            return View(_context.DoctorDepartments);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name", "Fee", "Degree", "PassingInstitute", "VisitingHour", "RoomNo", "ForAppointment", "DoctorDepartmentId")] Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var u = _context.Doctors/*.Include(u => u.DoctorDepartment)*/.Where(u => u.Id == id).FirstOrDefault();
            return View(u);
        }

        public IActionResult Edit(int id)
        {

            var doc = _context.Doctors.Find(id);
            var dd = _context.DoctorDepartments;
            ViewBag.DoctorDepartments = dd;

            return View(doc);
        }

        [HttpPost]
        public IActionResult Edit(int Id, [Bind("Id","Name","Degree","Fee", "PassingInstitute", "VisitingHour", "RoomNo", "ForAppointment", "DoctorDepartmentId")] Doctor doctor)
        {
            if(Id != doctor.Id)
            {
                return NotFound();
            }
            _context.Update(doctor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            
            var u = _context.Doctors.Where(u => u.Id == id).SingleOrDefault();
            //FirstOrDefault


            return View(u);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            _context.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
