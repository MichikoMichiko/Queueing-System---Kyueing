using Kyueng.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kyueng.Controllers
{
    public class QueueTicketController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QueueTicketController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var tickets = _db.QueueTickets.ToList();
            return View(tickets); // Pass List<QueueTicket> to the view
        }
    }

}
