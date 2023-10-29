using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.Title = upsertNewsContentRequest.Title;
            this.Description = upsertNewsContentRequest.Description;
            this.Image = new BaseImage(upsertNewsContentRequest.ImageId);
            this.SubCategoryId = upsertNewsContentRequest.SubCategoryId;
            this.Text = upsertNewsContentRequest.Text;
            this.Type = ContentType.News;
            this.TypeId = (int)ContentType.News;
        }
   
        [Description("news_content_text")]
        public string Text { get; set; }
       
    }
}
