﻿using System;

namespace Gitria.Core.Models
{
    public class RepositoryStatistics : BaseModel
    {
        public DateTime Week { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
    }
}