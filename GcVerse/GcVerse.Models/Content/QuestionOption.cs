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

        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        public int QuizzId { get; set; }
        
        public int ResultId { get; set; }

        public int ImageId { get; set; }
    }
}