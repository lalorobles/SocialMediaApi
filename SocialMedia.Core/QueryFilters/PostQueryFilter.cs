using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilter
    {
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public String Description { get; set; }
    }
}
