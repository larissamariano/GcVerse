using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuizzResult : BaseInfo
    {
        public QuizzResult()
        {
                
        }
        public QuizzResult(QuizzResultRequest quizzResultRequest)
        {
            this.Title = quizzResultRequest.Title;
            this.Description = quizzResultRequest.Description;
            this.Image = new BaseImage(quizzResultRequest.ImageId);
            this.ImageId = quizzResultRequest.ImageId;
        }

        [Description("result_id")]
        public int ResultId { get; set; }

        [Description("base_content_id")]
        public int QuizzId { get; set; }

        [Description("image_id")]
        public int ImageId { get; set; }

    }
}
