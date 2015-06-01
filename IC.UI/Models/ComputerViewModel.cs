using System;

namespace IC.UI.Models
{
    public class ComputerViewModel
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public Int64 Room { get; set; }
        public String IpAddress { get; set; }
        public String NetworkAddress { get; set; }
        public String SubnetAddress { get; set; }
    }
}