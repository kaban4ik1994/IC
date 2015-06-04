using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace IC.UI.Models
{
    public class StudentEditViewModel
    {
        public StudentEditViewModel()
        {
            var temp = new List<ListItem>();
            for (var i = 0; i < 12; i++)
            {
                var year = (DateTime.Now.Year - 6 + i).ToString();
                temp.Add(new ListItem { Text = year, Value = year });
            }
            YearOfEntrances = new SelectList(temp);

        }

        [HiddenInput]
        public long StudentId { get; set; }

        [HiddenInput]
        public long CourseId { get; set; }

        [Display(Name = "Courses")]
        public SelectList Courses { get; set; }

        [HiddenInput]
        public long SpecialtyId { get; set; }

        [Display(Name = "Specialties")]
        public SelectList Specialties { get; set; }

        [Display(Name = "Groups")]
        public SelectList Groups { get; set; }

        [HiddenInput]
        public long GroupId { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public SelectList YearOfEntrances { get; set; }
        [Display(Name = "Year Of Entrance")]
        public string YearOfEntrance { get; set; }
        [HiddenInput]
        public string Login { get; set; }
        [HiddenInput]
        public string Password { get; set; }
    }
}