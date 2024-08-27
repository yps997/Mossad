using Mossad.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mossad.Models
{
    public class Agent : IEntity<Location>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // false == not in mision, True == in mission
        public bool Status { get; set; } = false;
        public string Image { get; set; }

        public int? BodyCount { get; set; }
        public Location? _Location { get; set; }
    }
}
