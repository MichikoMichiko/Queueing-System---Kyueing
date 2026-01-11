using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyueng.Models
{
    public class QueueCall
    {
        public int Id { get; set; }

        public int? QueueTicketId { get; set; }

        [ForeignKey("QueueTicketId")]
        public required QueueTicket? QueueTicket { get; set; }

        public int? CounterId { get; set; }
        public Counter? Counter { get; set; }

        public DateTime CalledAt { get; set; } = DateTime.Now;
        
    }



}
