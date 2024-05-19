using HospitalManagement.Data;
using HospitalManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EntityModels;

namespace HospitalManagement.Controllers
{
    public class RoleController : Controller
    {
        public readonly _DbContext _context;

        public RoleController(_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1,int pageSize=5,string search="" )
        {
            var roles = _context.Roles;

            Pagination p=new Pagination(roles.Count(),page, pageSize);
            ViewBag.Pagination = p;
            var result=roles.Skip(p.Skip).Take(pageSize).ToList<Role>();

            return View(result);
        }


        [ActionName("Create")]
        public IActionResult Save()
        {
            return View(_context.Roles);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind( "Name")] Role role)
        {

            
            _context.Roles.Add(role);
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var u = _context.Roles.Include(u => u.Users).Where(u => u.Id == id).FirstOrDefault();
            return View(u);
        }


        public IActionResult Edit(int id)
        {
            
            var role = _context.Roles.Find(id);
            
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id", "Name")] Role role)
        {

            if (Id != role.Id)
            {
                return NotFound();
            }

            _context.Update(role);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            //var u = _context.Roles.Include(u => u.Users).Where(u => u.Id == id).SingleOrDefault<Role>();
            var u = _context.Roles.Where(u => u.Id == id).SingleOrDefault();
            //FirstOrDefault
            

            return View(u);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _context.Roles.Find(id);
            _context.Remove(role);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
