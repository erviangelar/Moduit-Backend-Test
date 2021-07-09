using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduit.Interview.Models
{
    
    public class QuestionsModel
    {
        public class List
        {
            public List<Data> data { get; set; }
        }
        public class Data
        {
            public long id { get; set; }
            public long category { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string footer { get; set; }
            public DateTime createdAt { get; set; }
        }
    }
}
