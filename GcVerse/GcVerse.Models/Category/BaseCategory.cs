using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Category
{
    public class BaseCategory : BaseInfo
    {
        public BaseCategory()
        {
                
        }

        public BaseCategory(UpsertCategoryRequest createCategoryRequest)
        {
            this.Id = Guid.NewGuid();
            this.Description = createCategoryRequest.Description;
            this.Title = createCategoryRequest.Title;
            this.Images = createCategoryRequest.Images;
        }

        public Guid Id { get; set; }
    }
}
