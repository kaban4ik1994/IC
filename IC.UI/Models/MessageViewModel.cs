using System;
using System.ComponentModel.DataAnnotations;

namespace IC.UI.Models
{
    public class MessageViewModel
    {
        public long MessageId { get; set; }
        [Display(Name = "From")]
        public string From { get; set; }
        [Display(Name = "To")]
        public string To { get; set; }
        [Display(Name = "Context")]
        public string Context { get; set; }
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "DispatchDate")]
        public DateTime DispatchDate { get; set; }
    }
}