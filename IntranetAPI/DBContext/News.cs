using System;
using System.Collections.Generic;

#nullable disable

namespace IntranetAPI.DBContext
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
