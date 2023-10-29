using GcVerse.Models.Content;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class UpsertQuizzContentRequest : BaseInfoRequest
    {
        public int SubCategoryId { get; set; }
        public List<QuizzQuestion> Questions { get; set; }
        public List<QuizzResults> Results { get; set; }
    }
}
