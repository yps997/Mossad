using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mossad.Enum;

namespace Mossad.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Agent")]
        public int? AgentId { get; set; }
        
        [ForeignKey("Target")]
        public int? TargetId { get; set; }

        public string Timer { get; set; }

        public TimeSpan? ActualTime {  get; set; }
        
        public MissionsStatus Status { get; set; }
        
        [NotMapped]
        public DateTime? StartTime { get; set; }

    }
}
