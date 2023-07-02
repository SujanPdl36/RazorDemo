using System.ComponentModel.DataAnnotations;

namespace RazorDemo.Model.View
{
    public class NewRegister
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
