using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            HasKey(message => message.MessageId);

            Property(message => message.MessageId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(message => message.Context).IsRequired();
            Property(message => message.IsViewed).IsRequired();
            Property(message => message.DispatchDate).HasColumnType("datetime2").IsRequired();

            ToTable("Messages");

            HasRequired(entity => entity.FromUser)
                .WithMany(user => user.SentMessages)
                .HasForeignKey(message => message.FromUserId)
                .WillCascadeOnDelete(false);
            HasRequired(entity => entity.ToUser)
                .WithMany(user => user.ReceivedMessages)
                .HasForeignKey(message => message.ToUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
