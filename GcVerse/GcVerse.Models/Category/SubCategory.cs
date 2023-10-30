using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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

        public SubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            this.CategoryId = upsertSubCategoryRequest.CategoryId;
            this.Related = upsertSubCategoryRequest.Related;
            this.Description = upsertSubCategoryRequest.Description;
            this.Image = new BaseImage(upsertSubCategoryRequest.ImageId);
            this.Title = upsertSubCategoryRequest.Title;
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Related { get; set; }
    }
}