using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuizzContent : BaseContent
    {
        public List<QuizzQuestion> Questions { get; set; }
        public List<QuizzResults> Results { get; set; }

    }
}
