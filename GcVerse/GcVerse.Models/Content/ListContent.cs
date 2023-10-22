using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class ListContent : BaseContent
    {
        public ListContent()
        {
                
        }

        public ListContent(CreateListCotentRequest createListCotentRequest)
        {
            this.Id = Guid.NewGuid();
            this.Topics = createListCotentRequest.Topics;
            this.Title = createListCotentRequest.Title;
            this.Description = createListCotentRequest.Description;
            this.Images = createListCotentRequest.Images;
            this.SubCategoryId = createListCotentRequest.SubCategoryId;
        }

        public List<BaseInfo> Topics { get; set; }
    }
}