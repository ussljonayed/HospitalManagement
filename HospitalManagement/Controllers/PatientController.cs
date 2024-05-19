using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using HospitalManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HospitalManagement.Controllers
{
    public class PatientController : Controller
    {

        public readonly _DbContext _context;

        public PatientController(_DbContext context)
        {
            _context = context;

        }
        //public IActionResult Index()
        //{
        //    var patients = _context.Patients.Include(u => u.Doctor);
        //    return View(patients);
        //}


        public IActionResult Index(int page = 1, int pageSize = 5, string search = "")
        {
            var patients = _context.Patients;

            Pagination p = new Pagination(patients.Count(), page, pageSize);
            ViewBag.Pagination = p;
            var result = patients.Skip(p.Skip).Take(pageSize).ToList<Patient>();

            return View(result);
        }



        [ActionName("Create")]

        public IActionResult Save()
        {
            return View(_context.Doctors);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name", "Dob", "Mobile", "Gender", "Profession", "Weight")] Patient patient)
        {
            _context.Patients.Add(patient);
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var p = _context.Patients.Where(p => p.Id == id).FirstOrDefault();
            return View(p);
        }


        public IActionResult Edit(int id)
        {

            var p = _context.Patients.Find(id);          

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(int Id, [Bind("Id", "Name", "Dob", "Mobile", "Gender", "Profession", "Weight")] Patient patient)
        {
            if (Id != patient.Id)
            {
                return NotFound();
            }
            _context.Update(patient);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {

            var p = _context.Patients.Where(p => p.Id == id).SingleOrDefault();
            
            return View(p);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            _context.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }

}
