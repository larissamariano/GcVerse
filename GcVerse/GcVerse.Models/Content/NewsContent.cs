using GcVerse.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class NewsContent : BaseContent
    {
        public NewsContent()
        {

        }

        public NewsContent(UpsertNewsContentRequest upsertNewsContentRequest)
        {
            this.Id = Guid.NewGuid();
            this.Title = upsertNewsContentRequest.Title;
            this.Description = upsertNewsContentRequest.Description;
            this.Image = upsertNewsContentRequest.Image;
            this.SubCategoryId = upsertNewsContentRequest.SubCategoryId;
            this.Text = upsertNewsContentRequest.Text;
        }
        public List<string> Text { get; set; }
    }
}
