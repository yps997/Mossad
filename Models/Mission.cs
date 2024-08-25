using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime? Timer { get; set; }
        public DateTime? ActualTime {  get; set; } 
        public string Status { get; set; }

    }
}
