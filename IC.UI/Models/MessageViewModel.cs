using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class MessageViewModel
    {
        public long MessageId { get; set; }
        [Display(Name = "From:")]
        public string From { get; set; }
        [Required(ErrorMessage = "Fill in the field.")]
        [Remote("CorrectEmailAddress", "Validation", AdditionalFields = "From",ErrorMessage = "Invalid Email.")]
        [Display(Name = "To:")]
        public string To { get; set; }
        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Context:")]
        public string Context { get; set; }
        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Subject:")]
        public string Subject { get; set; }
        [Display(Name = "DispatchDate:")]
        public DateTime DispatchDate { get; set; }
        public long FromId { get; set; }
        public long ToId { get; set; }
        public bool IsNew { get; set; }
    }
}