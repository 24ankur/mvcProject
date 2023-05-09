using System.ComponentModel.DataAnnotations;

namespace SharedLogic.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name ="User Name")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Must Contain at least 5 character")]
        [RegularExpression("((?=.*[@#$%]).{5,20})", ErrorMessage = "Must Contain atleast one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
