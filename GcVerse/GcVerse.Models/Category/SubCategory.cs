using GcVerse.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Category
{
    public class SubCategory : BaseCategory
    {
        public SubCategory()
        {
            
        }

        public SubCategory(UpsertSubCategoryRequest createSubCategoryRequest)
        {
            this.Id = Guid.NewGuid();
            this.CategoryId = createSubCategoryRequest.CategoryId;
            this.Related = createSubCategoryRequest.Related;
            this.Description = createSubCategoryRequest.Description;
            this.Images = createSubCategoryRequest.Images;
            this.Title = createSubCategoryRequest.Title;
        }

        public Guid CategoryId { get; set; }
        public List<string> Related { get; set; }
    }
}