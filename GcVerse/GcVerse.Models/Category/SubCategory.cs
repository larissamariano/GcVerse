using GcVerse.Models.Request;
using System;
using System.Collections.Generic;
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

        public SubCategory(UpsertSubCategoryRequest createSubCategoryRequest)
        {
            this.CategoryId = createSubCategoryRequest.CategoryId;
            this.Related = createSubCategoryRequest.Related;
            this.Description = createSubCategoryRequest.Description;
            this.Image = createSubCategoryRequest.Image;
            this.Title = createSubCategoryRequest.Title;
        }

        [Column("sub_category_id")]
        public int Id { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("related_info")]
        public string Related { get; set; }
    }
}