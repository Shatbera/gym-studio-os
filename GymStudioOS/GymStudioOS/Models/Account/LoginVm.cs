using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Account
{
    public class LoginVm
    {
        [Required] 
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
