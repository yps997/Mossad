using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mossad.Modles
{
    public class Mission
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        
        [ForeignKey("Target")]
        public Guid? TargetId { get; set; }
        public DateTime? Timer { get; set; }
        public DateTime? ActualTime {  get; set; } 
        public string Status { get; set; }

        public Mission()
        {
            Id = Guid.NewGuid();
        }
    }
}
