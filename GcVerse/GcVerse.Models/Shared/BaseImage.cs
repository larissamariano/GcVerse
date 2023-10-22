using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Shared
{
    public class BaseImage
    {
        public string RelatedId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
    }
}
