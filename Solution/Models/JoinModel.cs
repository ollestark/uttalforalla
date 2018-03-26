using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Models
{
    public class JoinModel
    {
        public string categoryName { get; set; }
        public string segmentName { get; set; }
        public string subCategoryName { get; set; }
        public int subCategoryId { get; set; }
        public int categoryID { get; set; }
        public int segmentID { get; set; }
        public string assignmentName { get; set; }
        public int assignmentID { get; set; }
    }
}