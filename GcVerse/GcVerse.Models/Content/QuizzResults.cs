﻿using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Content
{
    public class QuizzResults : BaseInfo
    {
        public Guid ResultId { get; set; }
        public Guid QuizzId { get; set; }
    }
}