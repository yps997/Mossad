using System.ComponentModel.DataAnnotations;

namespace Mossad.Modles
{
    public class Target
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        // false == dead, true == live
        public bool Status { get; set; }
        public string? Image { get; set; }
        public Location? Location { get; set; }

        public Target()
        {
            Id = Guid.NewGuid();
        }
    }
}
