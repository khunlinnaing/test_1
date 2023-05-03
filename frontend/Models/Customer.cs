using System;
using System.ComponentModel.DataAnnotations;
namespace frontend
{
    public class Customer
    {
        public int id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter only numbers")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Profile is required.")]
        public string? Profile { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date)]
        public string? Birdthday { get; set; }
    }
}