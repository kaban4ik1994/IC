using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class HistoryMap : EntityTypeConfiguration<History>
    {
        public HistoryMap()
        {
            HasKey(history => history.HistoryId);

            Property(history => history.HistoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(history => history.Email).IsRequired();
            Property(history => history.DateTime).HasColumnType("datetime2").IsRequired();
            Property(history => history.Action).IsRequired();
            Property(history => history.Entity).IsRequired();

            ToTable("History");
        }
    }
}
