using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class BaseInfoRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int ImageId { get; set; }
    }
}
