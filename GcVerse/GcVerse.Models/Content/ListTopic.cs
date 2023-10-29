using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class ListTopic : BaseInfo
    {
        [Description("topic_id")]
        public int Id { get; set; }

        [Description("base_content_id")]
        public int BaseContentId { get; set; }

        [Description("image_id")]
        public int ImageId { get; set; }
    }
}
