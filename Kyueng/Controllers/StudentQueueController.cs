using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

using Kyueng.Data;
using Kyueng.Models;


namespace Kyueng.Controllers
{
    public class StudentQueueController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentQueueController(ApplicationDbContext db)
        {
            ApplicationDbContext applicationDbContext = _db = db;
            _ = applicationDbContext;
        }


        [HttpPost]
        public IActionResult TapRFID(string studentId)
        {
            var student = _db.Students.FirstOrDefault(s => s.RFIDUID == studentId);
            if (student == null)
            {
                TempData["Message"] = "RFID not registered.";
                return RedirectToAction("TapRFID");
            }

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var existing = _db.QueueTickets
     .Any(q =>
         q.StudentId == student.Id &&
         !q.IsCalled &&
         q.CreatedAt.Date == DateTime.Today);

            if (existing)
            {
                TempData["Message"] = "You are already in the queue.";
                return RedirectToAction("TapRFID");
            }



            // Generate next ticket number
            var lastTicket = _db.QueueTickets
                .Where(q => q.CreatedAt >= today && q.CreatedAt < tomorrow)
                .OrderByDescending(q => q.CreatedAt)
                .FirstOrDefault();

            int nextNum = 1;
            if (lastTicket?.TicketNumber is string last && last.Length > 1)
            {
                var digits = new string(last.Skip(1).ToArray());
                if (int.TryParse(digits, out int lastNum))
                    nextNum = lastNum + 1;
            }

            var ticket = new QueueTicket
            {
                TicketNumber = $"A{nextNum:D3}",
                CreatedAt = DateTime.Now,
                IsCalled = false,
                StudentId = student.Id
            };

            _db.QueueTickets.Add(ticket);
            _db.SaveChanges();

            TempData["Message"] = $"You have joined the queue! Your ticket number is {ticket.TicketNumber}.";

          
            return RedirectToAction("TapRFID");

        }

        [HttpPost]
        public IActionResult CleanStaleTickets()
        {
            var stale = _db.QueueTickets
                .Where(q => q.CreatedAt < DateTime.Today)
                .ToList();

            _db.QueueTickets.RemoveRange(stale);
            _db.SaveChanges();

            TempData["Message"] = $"🧹 {stale.Count} stale tickets removed.";
            return RedirectToAction("OfficeDeskDashboard");
        }



    }
}