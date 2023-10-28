using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.Description = createCategoryRequest.Description;
            this.Title = createCategoryRequest.Title;
            this.Image = createCategoryRequest.Image;
        }

        [Column("category_id")]
        public int Id { get; set; }
    }
}
