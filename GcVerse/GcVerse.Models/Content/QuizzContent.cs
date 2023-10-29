using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuizzContent : BaseContent
    {
        public QuizzContent()
        {

        }

        public QuizzContent(UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            this.Title = upsertQuizzContentRequest.Title;
            this.Description = upsertQuizzContentRequest.Description;
            this.Image = new BaseImage(upsertQuizzContentRequest.ImageId);
            this.SubCategoryId = upsertQuizzContentRequest.SubCategoryId;
            this.Questions = upsertQuizzContentRequest.Questions.Select(q => new QuizzQuestion(q)).ToList();
            this.Results = upsertQuizzContentRequest.Results.Select(r => new QuizzResult(r)).ToList();
            this.Type = ContentType.Quizz;
            this.TypeId = (int)ContentType.Quizz;
        }

        public QuizzContent(BaseContent baseContent, List<QuizzQuestion> questions, List<QuizzResult> results)
        {
            this.Id = baseContent.Id;
            this.Title = baseContent.Title;
            this.Description = baseContent.Description;
            this.Image = baseContent.Image;
            this.SubCategoryId = baseContent.SubCategoryId;
            this.Type = baseContent.Type;
            this.TypeId = baseContent.TypeId;
            this.Questions = questions;
            this.Results = results;
        }

        public List<QuizzQuestion> Questions { get; set; }
        public List<QuizzResult> Results { get; set; }
    }
}