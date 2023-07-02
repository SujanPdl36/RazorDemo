using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RazorPageDemo.Model
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

 
    }


}
