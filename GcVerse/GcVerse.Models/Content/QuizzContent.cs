using GcVerse.Models.Request;
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
            this.Id = Guid.NewGuid();
            this.Title = upsertQuizzContentRequest.Title;
            this.Description = upsertQuizzContentRequest.Description;
            this.Images = upsertQuizzContentRequest.Images;
            this.SubCategoryId = upsertQuizzContentRequest.SubCategoryId;
            this.Questions = upsertQuizzContentRequest.Questions;
            this.Results = upsertQuizzContentRequest.Results;
        }

        public List<QuizzQuestion> Questions { get; set; }
        public List<QuizzResults> Results { get; set; }
    }
}
