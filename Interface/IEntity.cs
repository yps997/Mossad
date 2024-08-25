using Microsoft.CodeAnalysis;
using Mossad.Modles;
namespace Mossad.Interface
{
    public interface IEntity<Location>
    {
        int Id { get; set; }
        string Name { get; set; }
        Location? _Location { get; set; }
    }
}
