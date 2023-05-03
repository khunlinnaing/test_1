using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class Employees
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "The LastName field is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The PhoneNumber field is required.")]
        [RegularExpression(@"^\d{8,11}$", ErrorMessage = "Please enter a valid number between 8 to 11 digits.")]
        [StringLength(10, MinimumLength = 8)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "The Address field is required.")]
        public string? Address { get; set; }
        //[Required(ErrorMessage = "The Profile field is required.")]
        public string? Profile { get; set; }
        [Required(ErrorMessage = "The Birdthday field is required.")]
        public string? Birdthday { get; set; }
        [Required(ErrorMessage = "The Password field is required.")]
        [MinLength(8)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        [DefaultValue(0)]
        public int Role { get; set; }
    }
}