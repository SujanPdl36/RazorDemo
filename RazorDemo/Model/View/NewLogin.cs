using System.ComponentModel.DataAnnotations;

namespace RazorDemo.Model.View
{
    public class newLogin
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
