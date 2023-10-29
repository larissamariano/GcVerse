using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Shared
{
    public class BaseImage
    {
        public BaseImage()
        {
                
        }
        public BaseImage(int imageId)
        {
            Id = imageId;
        }

        [Description("image_id")]
        public long Id { get; set; }

        [Description("image_related_id")]
        public string RelatedId { get; set; }

        [Description("image_url")]
        public string Url { get; set; }

        [Description("image_description")]
        public string Description { get; set; }

        [Description("image_from_url")]
        public string FromUrl { get; set; }
    }
}
 