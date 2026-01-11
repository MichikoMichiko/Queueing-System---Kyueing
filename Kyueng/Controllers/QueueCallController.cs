using Kyueng.Data;
using Kyueng.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Kyueng.Hubs;
using Microsoft.EntityFrameworkCore; 


namespace Kyueng.Controllers
{
    public class QueueCallController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IHubContext<QueueHub> _hubContext;

        public QueueCallController(ApplicationDbContext db, IHubContext<QueueHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }
        // Display the dashboard with all tickets
        public IActionResult OfficeDeskDashboard()
        {
            // Include Student info in the queue tickets
            var tickets = _db.QueueTickets
       .Where(t => !t.IsCalled)
       .Include(t => t.Student)
       .OrderBy(t => t.CreatedAt)
       .ToList();



            //ViewBag.SkippedTickets = tickets.Where(t => t.WasSkipped).ToList(); // ✅ Filter skipped

            // Include Student in current called ticket as well (to avoid null in ViewBag)
            ViewBag.CurrentCalled = _db.QueueTickets
               .Where(t => t.IsCalled && t.CreatedAt.Date == DateTime.Today)
                .Include(t => t.Student)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefault();

            ViewBag.SkippedTickets = _db.QueueTickets
    .Where(t => t.WasSkipped && t.CreatedAt.Date == DateTime.Today)
    .Include(t => t.Student)
    .OrderByDescending(t => t.CreatedAt)
    .ToList();

            // ✅ Called tickets (excluding skipped ones)
            ViewBag.CalledTickets = _db.QueueTickets
                .Where(t => t.IsCalled && !t.WasSkipped && t.CreatedAt.Date == DateTime.Today)
                .Include(t => t.Student)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();

            return View(tickets);
        }




        // Call a specific ticket

        [HttpGet]
        public async Task<IActionResult> CallNext(int ticketId)
        {
            var ticket = _db.QueueTickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null)
                return RedirectToAction("OfficeDeskDashboard");

            ticket.IsCalled = true; 

            var queueCall = new QueueCall
            {
                QueueTicketId = ticket.Id,
                QueueTicket = ticket,
                CalledAt = DateTime.Now
            };

            _db.QueueCalls.Add(queueCall);
            await _db.SaveChangesAsync(); 

            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", ticket.TicketNumber);

            return RedirectToAction("OfficeDeskDashboard");
        }




        [HttpGet]
        public async Task<IActionResult> SkipTicket(int ticketId)
        {
            var ticket = _db.QueueTickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null || ticket.IsCalled)
                return RedirectToAction("OfficeDeskDashboard");

            ticket.IsCalled = true;
            ticket.WasSkipped = true; 

            await _db.SaveChangesAsync();

            return RedirectToAction("OfficeDeskDashboard");
        }





    }
}
