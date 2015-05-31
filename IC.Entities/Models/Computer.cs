using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Computer : Entity
    {
        public long ComputerId { get; set; }
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public byte[] IpAddress { get; set; }
        public byte[] NetworkAddress { get; set; }
        public byte[] SubnetAddress { get; set; }
    }
}
