using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Shared
{
    public class BaseInfo
    {
        [Description("title")]
        public string Title { get; set; }

        [Description("description")]
        public string Description { get; set; }

        public BaseImage Image { get; set; }
    }
}
