using System.ComponentModel.DataAnnotations;

namespace Kyueng.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string Year { get; set; }  

        [Required]
        public string RFIDUID { get; set; }
    }


}
