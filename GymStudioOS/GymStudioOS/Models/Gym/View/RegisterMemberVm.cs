using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Gym.View
{
    public class RegisterMemberVm
    {
        // Search term entered by user
        public string? SearchTerm { get; set; }
        // Indicates if a search was performed
        public bool SearchPerformed { get; set; }
        public bool ShowRegisterForm { get; set; }
        // If found, user info
        public string SelectedRole { get; set; } = string.Empty;
        public int GymId { get; set; }
        public int? BranchId { get; set; }
        public UserProfileVM? FoundUser { get; set; }
        // For new user registration
        public UserProfileVM NewUser { get; set; } = new UserProfileVM();
    }
}
