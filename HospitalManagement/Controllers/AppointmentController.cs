using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using HospitalManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HospitalManagement.Controllers
{
    public class AppointmentController : Controller
    {
        public readonly _DbContext _context;

        public AppointmentController(_DbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    var appointments = _context.Appointments;
            
        //    return Ok(appointments);
        //}


        public IActionResult Index(int page = 1, int pageSize = 5, string search = "")
        {
            var appointments = _context.Appointments.Include(u => u.Patient).Include(d =>d.Doctor);


            Pagination p = new Pagination(appointments.Count(), page, pageSize);
            ViewBag.Pagination = p;
            var result = appointments.Skip(p.Skip).Take(pageSize).ToList<Appointment>();

            return View(result);
        }



        [ActionName("Create")]

        public IActionResult Save(int id)
        {
            var app = _context.Appointments.Find(id);
            var p = _context.Patients;
            ViewBag.Patients = p;

            var d = _context.Doctors;
            ViewBag.Doctors = d;
            return View(app);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("PatientId", "DoctorId", "Fee", "Date", "Time")] Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult Details(int id)
        {
            var a = _context.Appointments.Include(u => u.Doctor).Include(p => p.Patient).Where(a => a.Id == id).FirstOrDefault();
            return View(a);
        }

        public IActionResult Edit(int id)
        {


            var app = _context.Appointments.Find(id);
            var p = _context.Patients;
            ViewBag.Patients = p;

            var d = _context.Doctors;
            ViewBag.Doctors = d;

            return View(app);
        }


        [HttpPost]
        public IActionResult Edit(int Id, [Bind("Id", "PatientId", "DoctorId", "Fee", "Date", "Time")] Appointment appointment)
        {
            if (Id != appointment.Id)
            {
                return NotFound();
            }
            _context.Update(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {

            var a = _context.Appointments.Where(a => a.Id == id).SingleOrDefault();
            //FirstOrDefault


            return View(a);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            _context.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


    }
}
