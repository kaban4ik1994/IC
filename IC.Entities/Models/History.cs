using System;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public class History : Entity
    {
        public long HistoryId { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public Action Action { get; set; }
        public EntityEnum Entity { get; set; }
    }
}
