using Kyueng.Data;
using Kyueng.Hubs;
using Kyueng.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace Kyueng.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<QueueHub> _hubContext;



        public AdminController(ApplicationDbContext db, IHubContext<QueueHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;

        }

        public IActionResult Dashboard()
        {
            var tickets = _db.QueueTickets
                            .Include(q => q.Student) 
                            .OrderByDescending(q => q.CreatedAt)
                            .ToList();

            return View(tickets);
        }


        public IActionResult ViewStudents()
        {
            var students = _db.Students.ToList();
            return View(students);
        }

        public IActionResult History()
        {
            var history = _db.QueueCalls
                .Include(q => q.QueueTicket)
                    .ThenInclude(t => t.Student)
                .OrderByDescending(q => q.CalledAt)
                .ToList();

            return View(history);
        }




    }
}
