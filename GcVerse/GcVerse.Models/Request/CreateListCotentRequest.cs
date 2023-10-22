using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class CreateListCotentRequest : BaseInfo
    {
        public Guid SubCategoryId { get; set; }
        public List<BaseInfo> Topics { get; set; }
    }
}
