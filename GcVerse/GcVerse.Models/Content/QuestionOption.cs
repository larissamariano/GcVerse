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
    public class QuestionOption : BaseInfo
    {
        public QuestionOption()
        {
                
        }
        public QuestionOption(QuestionOptionRequest questionOptionRequest)
        {
            this.Title = questionOptionRequest.Title; 
            this.Description = questionOptionRequest.Description;
            this.Image = new BaseImage(questionOptionRequest.ImageId);
            this.ImageId = questionOptionRequest.ImageId;
        }

        [Description("option_id")]
        public int OptionId { get; set; }

        [Description("question_id")]
        public int QuestionId { get; set; }

        [Description("base_content_id")]
        public int QuizzId { get; set; }
        
        [Description("result_id")]
        public int ResultId { get; set; }

        [Description("image_id")]
        public int ImageId { get; set; }
    }
}
