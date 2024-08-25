using Mossad.Interface;
using System.ComponentModel.DataAnnotations;

namespace Mossad.Modles
{
    public class Agent : IEntity<Location>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // false == not in mision, True == in mission
        public bool Status { get; set; }
        public string Image { get; set; }
        public Location? _Location { get; set; }
        
    }
}
