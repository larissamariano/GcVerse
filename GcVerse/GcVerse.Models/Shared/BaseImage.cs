namespace GcVerse.Models.Shared
{
    public class BaseImage
    {
        public BaseImage()
        {
                
        }
        public BaseImage(int imageId)
        {
            Id = imageId;
        }

        public long Id { get; set; }

        public string RelatedId { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string FromUrl { get; set; }
    }
}
 