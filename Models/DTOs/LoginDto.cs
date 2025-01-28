using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username es Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password es Requerido")]  
        public string Password { get; set; }
    }
}
