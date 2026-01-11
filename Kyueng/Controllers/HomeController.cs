using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kyueng.Data;
using Kyueng.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.SignalR;
using Kyueng.Hubs;
using Microsoft.AspNetCore.Authorization;
using MyLibrary;

namespace Kyueng.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<QueueHub> _hubContext;

        public HomeController(ApplicationDbContext db, IHubContext<QueueHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        [Authorize]
        public IActionResult Index()
        {

            //Now Serving
            ViewBag.NowServing = _db.QueueCalls
    .Where(qc => qc.CalledAt.Date == DateTime.Today) 
    .OrderByDescending(qc => qc.CalledAt)
    .Select(qc => qc.QueueTicket.TicketNumber)
    .FirstOrDefault() ?? "----"; 


            //Last Queue Number Today
            ViewBag.LastTicket = _db.QueueTickets
    .Where(q => q.CreatedAt.Date == DateTime.Today && !q.WasSkipped)
    .OrderByDescending(q => q.CreatedAt)
    .Select(q => q.TicketNumber)
    .FirstOrDefault() ?? "Waiting";

           

            ViewBag.Greeting = Tools.Greet("Visitor");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TapRFIDAsync(string rfid)
        {

            // Now Serving
            ViewBag.NowServing = _db.QueueCalls
    .Where(qc => qc.CalledAt.Date == DateTime.Today)
    .OrderByDescending(qc => qc.CalledAt)
    .Select(qc => qc.QueueTicket.TicketNumber)
    .FirstOrDefault();


            //Last Queue Number Today
            ViewBag.LastTicket = _db.QueueTickets
                .Where(q => q.CreatedAt.Date == DateTime.Today)
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => q.TicketNumber)
                .FirstOrDefault();
            var student = _db.Students.FirstOrDefault(s => s.RFIDUID == rfid);

            if (student == null)
            {
                ViewBag.Message = "RFID not registered. Please ask for an assistance";
                return View("Index");
            }

            var today = DateTime.Today;

            //  Only block students who have an active (not called) ticket today
            var existing = _db.QueueTickets
                .FirstOrDefault(q => q.StudentId == student.Id && q.CreatedAt.Date == today && !q.IsCalled && !q.WasSkipped);

            if (existing != null)
            {
                Console.WriteLine($"Student {student.FullName} already has ticket: {existing.TicketNumber}");
               // ViewBag.NowServing = "Waiting";
                ViewBag.Message = $"You're already in the queue! Ticket Number : {existing.TicketNumber}";
                return View("Index");
            }

            // Get the last ticket of today
            var last = _db.QueueTickets
                .Where(q => q.CreatedAt.Date == today)
                .OrderByDescending(q => q.CreatedAt)
                .FirstOrDefault();

            int nextNumber = 1;
            if (last != null && int.TryParse(last.TicketNumber.Substring(1), out int lastNum))
                nextNumber = lastNum + 1;

            string ticketStr = $"A{nextNumber:D3}";

            var ticket = new QueueTicket
            {
                TicketNumber = ticketStr,
                CreatedAt = DateTime.Now,
                IsCalled = false,
               
                StudentId = student.Id
            };

            _db.QueueTickets.Add(ticket);
            _db.SaveChanges();

            ViewBag.Message = Tools.Greet(student.FullName) +
                   $", your queue number is {ticket.TicketNumber}";


            await _hubContext.Clients.All.SendAsync("UpdateLastTicket", ticket.TicketNumber);
            await _hubContext.Clients.All.SendAsync("UpdateNowServing", ticket.TicketNumber);


            return RedirectToAction("Index");

        }



    }
}
