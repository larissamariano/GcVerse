using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class CreateSubCategoryRequest : BaseInfo
    {
        public Guid CategoryId { get; set; }
        public List<string> Related { get; set; }
    }
}