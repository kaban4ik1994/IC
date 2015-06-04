using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class ComputerEditViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid Room Number")]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Computer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^([0-9]|[0-9][0-9]|[01][0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[0-9][0-9]|[01][0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$", ErrorMessage = "Invalid Network Address")]
        [Display(Name = "Network Address")]
        public string NetworkAddress { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^(((255\.){3}(255|254|252|248|240|224|192|128|0+))|((255\.){2}(255|254|252|248|240|224|192|128|0+)\.0)|((255\.)(255|254|252|248|240|224|192|128|0+)(\.0+){2})|((255|254|252|248|240|224|192|128|0+)(\.0+){3}))$", ErrorMessage = "Invalid Subnet mask")]
        [Display(Name = "Subnet Mask")]
        public string NetworkMask { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^([0-9]|[0-9][0-9]|[01][0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[0-9][0-9]|[01][0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$", ErrorMessage = "Invalid Ip Address")]
        [Remote("CheckIpAddress", "Validation", AdditionalFields = "NetworkAddress,NetworkMask", ErrorMessage = "Is not compatible with network address and mask.")]
        [Display(Name = "Ip Address")]
        public string IpAddress { get; set; }
    }
}