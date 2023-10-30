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
    public class QuizzQuestion : BaseInfo
    {
        public QuizzQuestion()
        {
                
        }

        public QuizzQuestion(QuizzQuestionRequest quizzQuestionRequest)
        {
            this.Title = quizzQuestionRequest.Title;
            this.Description = quizzQuestionRequest.Description;
            this.Image = new BaseImage(quizzQuestionRequest.ImageId);
            this.Options = quizzQuestionRequest.Options.Select(o => new QuestionOption(o)).ToList();
        }

        public int QuestionId { get; set; }

        public int QuizzId { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
}
