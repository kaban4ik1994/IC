using System;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Message : Entity
    {

        public long MessageId { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public string Context { get; set; }
        public bool IsViewed { get; set; }
        public bool ShowForFirstUser { get; set; }
        public bool ShowForSecondUser { get; set; }
        public DateTime DispatchDate { get; set; }
        public string Subject { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}
