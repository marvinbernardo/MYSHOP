using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace My_Shop_MVC.Models
{
    public class UsersModel
    {
        [Key]
        [Display(Name ="User ID")]
        public int UsersID { get; set; }

        [Display(Name = "User Type")]
        [Required(ErrorMessage = "This is required.")]
        public int TypeID { get; set; }
        public List<SelectListItem> UserTypes { get; set; }
        public string UserType { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(100, ErrorMessage = "Icorrect input.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Incorrect format")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(20, ErrorMessage = "Icorrect input.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(50, ErrorMessage = "Icorrect input.")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This is required.")]
        [MaxLength(100, ErrorMessage = "Icorrect input.")]
        public string FirstName { get; set; }

        [Display(Name = "Street Address")]
        [MaxLength(50, ErrorMessage = "Icorrect input.")]
        public string Street { get; set; }

        [Display(Name = "Municipality")]
        [MaxLength(50, ErrorMessage = "Icorrect input.")]
        [DataType(DataType.MultilineText)]
        public string Municipality { get; set; }

        [Display(Name = "City")]
        [MaxLength(20, ErrorMessage = "Icorrect input.")]
        public string City { get; set; }

        [Display(Name = "Landline Number")]
        [MaxLength(12, ErrorMessage = "Icorrect input.")]
        public string Phone { get; set; }

        [Display(Name = "Cellphone Number")]
        [MaxLength(11, ErrorMessage = "Icorrect input.")]
        [RegularExpression(".{11}", ErrorMessage = "Incorrect format.")]
        public string MobilePhone { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
    }
}