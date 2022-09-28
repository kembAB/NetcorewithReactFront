using System.ComponentModel.DataAnnotations;

namespace MVCWebApp.Models.Person.ViewModels
{
    public class RegisterViewModel
    {
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }

        //    [Required]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [Compare("Password",
        //        ErrorMessage = "Password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)",
        ErrorMessage = "Please enter a valid birth date! example: dd/mm/yyyy")]
        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }
    }
}

