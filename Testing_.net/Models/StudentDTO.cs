using System.ComponentModel.DataAnnotations;
using Testing_.net.Validators;

namespace Testing_.net.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Enter Valid Email Address")]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }    
        [DateCheck]    
        public DateTime AdmissionDate { get; set; }
    }
}
