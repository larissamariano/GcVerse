using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class UpsertNewsContentRequest : BaseInfoRequest
    {
        public int SubCategoryId { get; set; }
        public string Text { get; set; }
    }
}