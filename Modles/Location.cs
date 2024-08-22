using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mossad.Modles
{
   public class Location
   {
        [Range(0,1000)]
        public int X { get; set; }
        
        [Range(0, 1000)]
        public int Y { get; set; }

    }


}

