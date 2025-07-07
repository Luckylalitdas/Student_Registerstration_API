using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Student_Registerstration.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        [PasswordPropertyText]
        public string? Password { get; set; }
        [Required(ErrorMessage = "password is required")]
        [PasswordPropertyText]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "phone number is required")]
        [Phone]
        [MaxLength(10, ErrorMessage = "Number should be 10 degites")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender? Gender { get; set; }
        [Required(ErrorMessage = "class is required")]
        public string? Class { get; set; }
        [Required(ErrorMessage = "parentesname is required")]
        public string? ParentesName { get; set; }
        [Required(ErrorMessage = " field is required")]
        [Phone]
        public string? ParentesPhoneno { get; set; }
    }
    public enum Gender
    {
        Male, Female
    }

}