using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class ComputerMap : EntityTypeConfiguration<Computer>
    {
        public ComputerMap()
        {
            HasKey(computer => computer.ComputerId);

            Property(computer => computer.ComputerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(computer => computer.Name).IsRequired();
            Property(computer => computer.RoomNumber).IsRequired();
            Property(computer => computer.IpAddress).IsRequired();
            Property(computer => computer.NetworkAddress).IsRequired();
            Property(computer => computer.SubnetAddress).IsRequired();

            ToTable("Computers");
        }
    }
}
