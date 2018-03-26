using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Solution.Models;

namespace Solution.Models
{
    public class JoinModelCategory
    {
       public Category categories{ get; set; }
       public Segment segment { get; set; }
    }
}