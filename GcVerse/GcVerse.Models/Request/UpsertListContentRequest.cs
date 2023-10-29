﻿using GcVerse.Models.Content;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class UpsertListContentRequest : BaseInfoRequest
    {
        public int SubCategoryId { get; set; }
        public List<ListTopic> Topics { get; set; }
    }
}
