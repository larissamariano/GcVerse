using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class QuizzQuestionRequest : BaseInfoRequest
    {
        public List<QuestionOptionRequest> Options { get; set; }
    }
}
