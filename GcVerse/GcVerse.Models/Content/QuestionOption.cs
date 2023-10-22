using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuestionOption : BaseInfo
    {
        public Guid OptionId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ResultId { get; set; }
    }
}
