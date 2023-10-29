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
    public class BaseCategory : BaseInfo
    {
        public BaseCategory()
        {
                
        }

        public BaseCategory(UpsertCategoryRequest upsertCategoryRequest)
        {
            this.Description = upsertCategoryRequest.Description;
            this.Title = upsertCategoryRequest.Title;
            this.Image = new BaseImage(upsertCategoryRequest.ImageId);
        }

        [Description("category_id")]
        public int Id { get; set; }
    }
}
