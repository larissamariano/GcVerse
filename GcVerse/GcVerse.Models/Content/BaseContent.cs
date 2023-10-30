using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class BaseContent : BaseInfo
    {
        public int Id { get; set; }

        public ContentType Type { get; set; }

        public int TypeId { get; set; }

        public int SubCategoryId { get; set; }
    }
}
