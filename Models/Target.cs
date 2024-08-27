using Mossad.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mossad.Enum;

namespace Mossad.Models
{
    public class Target : IEntity<Location>
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public TargetEnum Status { get; set; }
        public string? Image { get; set; }
        public int? ExterminatingAgent { get; set; }
        public Location? _Location { get; set; }

    }
}
