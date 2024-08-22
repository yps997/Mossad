using System.ComponentModel.DataAnnotations;

namespace Mossad.Modles
{
    public class Agent
    {    
        public Guid Id { get; set; }
        public string Name { get; set; }

        // false == not in mision, True == in mission
        public bool Status { get; set; }
        public string Image { get; set; }
        public Location? Location { get; set; }

        public Agent()
        {
            Id = Guid.NewGuid();
        }
        
    }
}
