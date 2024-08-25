using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mossad.Models
{
   public class Location
   {
        [Range(0,1000, ErrorMessage = "x point must be between {0} and {1000}.")]
        public int X { get; set; }

        [Range(0, 1000, ErrorMessage = "y point must be between {0} and {1000}.")]
        public int Y { get; set; }

    }


}

