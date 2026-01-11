using Microsoft.AspNetCore.Mvc;
using Kyueng.Data;
using Kyueng.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyueng.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext db, ApplicationDbContext context  )
        {
            _db = db;
            _context = context;
        }

        // GET: Student/Register
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ViewStudents(string search)
        {
            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                students = students.Where(s => s.FullName.Contains(search));
            }

            var sorted = students.OrderBy(s => s.FullName).ToList();

            return View(sorted);
        }



        // POST: Student/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Student student)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Please fill in all fields correctly.";
                return View(student);
            }

            var existing = _db.Students.FirstOrDefault(s =>
                s.StudentId == student.StudentId || s.RFIDUID == student.RFIDUID);

            if (existing != null)
            {
                TempData["Message"] = "Student is already registered.";
                return View(student);
            }

            try
            {
                _db.Students.Add(student);
                _db.SaveChanges();

                TempData["Message"] = "Student registered successfully!";
                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                TempData["Message"] = "Something went wrong: " + ex.Message;
                return View(student);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            // 🔍 Check if the student is still in the queue
            bool stillInQueue = _db.QueueTickets.Any(q => q.StudentId == id && !q.IsCalled);
            if (stillInQueue)
            {
                TempData["Error"] = "❌ This student is still in the queue. Please remove their ticket first.";
                return RedirectToAction("ViewStudents");
            }

            // ✅ Safe to delete
            _db.Students.Remove(student);
            _db.SaveChanges();

            TempData["Success"] = "✅ Student deleted successfully.";
            return RedirectToAction("ViewStudents");
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return PartialView("_EditStudentPartial", student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }

            // Save student changes...
            _context.Update(student);
            _context.SaveChanges();

            return Json(new { success = true });
        }





    }
}
