using HospitalManagement.Data;
using HospitalManagement.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using HospitalManagement.Models.ViewModels;
using System.Linq;

namespace HospitalManagement.Controllers
{
    public class UserController : Controller
    {
        public readonly _DbContext _context;
        public IWebHostEnvironment _host;

        public UserController(_DbContext context, IWebHostEnvironment host)
        {
            this._context = context;
            _host = host;
        }

        

        //Include Method


        // public async Task<IActionResult>Index()
        // {
        //     var a = _context.Roles.ToList();
        //     var b= _context.Users.ToList(); 
        //     return Ok(b); 
        // }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 4, string search = "")
        {
            var stopwatch = Stopwatch.StartNew();
            stopwatch.Start();



            var Result = _context.Users.Join(_context.Roles, u => u.RoleId, r => r.Id, (u, r) => new UserReport { Id = u.Id, Name = u.Name, Role = r.Name }).Where(p => p.Name.Contains(search));

            Pagination p = new Pagination(Result.Count(), page, pageSize);
            ViewBag.Pagination = p;

            var users = await Result.Skip(p.Skip).Take(pageSize).ToListAsync<UserReport>();


            stopwatch.Stop();
            long ms = stopwatch.ElapsedMilliseconds;
            ViewBag.ms = ms;

            return View(users);
        }






        [ActionName("Create")]
        public IActionResult Save()
        {
            return View(_context.Roles);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Name", "Password", "RoleId", "Email", "FullName", "ContactNo", "File")] User user)
        {

            _context.Users.Add(user);
            Upload(user.File, user.Name);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var u = _context.Users.Include(u => u.Role).Where(u => u.Id == id).FirstOrDefault();
            return View(u);
        }


        public IActionResult Edit(int id)
        {
            var u = _context.Users.Find(id);
            var r = _context.Roles;
            ViewBag.Roles = r;
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id", "Name", "Password", "RoleId", "Email", "FullName", "ContactNo", "File")] User user)
        {
            if (Id != user.Id)
            {
                return NotFound();
            }

            if (user.File != null)
            {
                user.Photo = user.Name + Path.GetExtension(user.File.FileName);
            }

            _context.Users.Update(user);
            _context.SaveChanges();

            //Photo Upload
            Upload(user.File, user.Name);

            return RedirectToAction(nameof(Index));
        }

        public void Upload(IFormFile file, String name)
        {
            if (file != null)
            {
                String ext = Path.GetExtension(file.FileName);
                using (FileStream fs = System.IO.File.Create($"{_host.ContentRootPath}/wwwroot/img/{name}{ext}"))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var u = _context.Users.Include(u => u.Role).Where(u => u.Id == id).SingleOrDefault<User>();

            return View(u);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
