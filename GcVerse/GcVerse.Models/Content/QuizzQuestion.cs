using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuizzQuestion : BaseInfo
    {
        public Guid QuestionId { get; set; }
        public Guid QuizzId { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
}
