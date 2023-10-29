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
        [Description("base_content_id")]
        public int Id { get; set; }

        public ContentType Type { get; set; }

        [Description("content_type_id")]
        public int TypeId { get; set; }

        [Description("sub_category_id")]
        public int SubCategoryId { get; set; }
    }
}
