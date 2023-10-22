using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Shared
{
    public class BaseInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<BaseImage> Images { get; set; }
    }
}
