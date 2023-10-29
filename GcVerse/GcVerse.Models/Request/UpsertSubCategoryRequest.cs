using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class UpsertSubCategoryRequest : BaseInfoRequest
    {
        public int CategoryId { get; set; }
        public string Related { get; set; }
    }
}