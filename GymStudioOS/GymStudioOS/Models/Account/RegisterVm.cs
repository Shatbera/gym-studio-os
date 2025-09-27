using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Account
{
    public class RegisterVm
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = "";

        [Required]
        [MaxLength(20)]
        public string PersonalId { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";

        public string Role { get; set; } = AppRoles.User;
    }
}
