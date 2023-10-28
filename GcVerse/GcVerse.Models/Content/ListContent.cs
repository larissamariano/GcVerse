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

        public ListContent(UpsertListContentRequest upsertListCotentRequest)
        {
            this.Id = Guid.NewGuid();
            this.Title = upsertListCotentRequest.Title;
            this.Description = upsertListCotentRequest.Description;
            this.Image = upsertListCotentRequest.Image;
            this.SubCategoryId = upsertListCotentRequest.SubCategoryId;
            this.Topics = upsertListCotentRequest.Topics;
        }

        public List<BaseInfo> Topics { get; set; }
    }
}