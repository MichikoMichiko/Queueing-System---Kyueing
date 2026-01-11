using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyueng.Models
{
    public class QueueTicket
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ticket Number")]
        public string TicketNumber { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Is Called")]
        public bool IsCalled { get; set; }

        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        public bool WasSkipped { get; set; }
    }
}
