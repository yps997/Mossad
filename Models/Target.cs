using Mossad.Interface;
using System.ComponentModel.DataAnnotations;

namespace Mossad.Models
{
    public class Target : IEntity<Location>
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        // false == dead, true == live
        public bool Status { get; set; }
        public string? Image { get; set; }
        public Location? _Location { get; set; }

    }
}
