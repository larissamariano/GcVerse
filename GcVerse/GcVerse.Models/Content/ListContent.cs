using GcVerse.Models.Request;
using GcVerse.Models.Shared;

namespace GcVerse.Models.Content
{
    public class ListContent : BaseContent
    {
        public ListContent()
        {
                
        }

        public ListContent(UpsertListContentRequest upsertListCotentRequest)
        {
            this.Title = upsertListCotentRequest.Title;
            this.Description = upsertListCotentRequest.Description;
            this.Image = new BaseImage(upsertListCotentRequest.ImageId);
            this.SubCategoryId = upsertListCotentRequest.SubCategoryId;
            this.Topics = upsertListCotentRequest.Topics;
            this.Type = ContentType.List;
            this.TypeId = (int)ContentType.List;
        }

        public ListContent(BaseContent baseContent, List<ListTopic> topics)
        {
            this.Id = baseContent.Id;
            this.Title = baseContent.Title;
            this.Description = baseContent.Description;
            this.Image = baseContent.Image;
            this.SubCategoryId = baseContent.SubCategoryId;
            this.Type = baseContent.Type;
            this.TypeId = baseContent.TypeId;
            this.Topics = topics;
        }

        public List<ListTopic> Topics { get; set; }
    }
}