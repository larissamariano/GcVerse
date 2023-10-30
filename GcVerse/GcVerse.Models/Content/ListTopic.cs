using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class ListTopic : BaseInfo
    {
        public int Id { get; set; }

        public int BaseContentId { get; set; }

        public int ImageId { get; set; }
    }
}
