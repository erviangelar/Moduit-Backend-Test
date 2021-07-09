using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduit.Interview.Requests
{
    public class QuestionRequest
    {
        public class Data
        {
            public int id { get; set; }
            public int category { get; set; }
            public List<Detail> items { get; set; }
            public DateTime createdAt { get; set; }
        }
        public class Detail
        {
            public string title { get; set; }
            public string description { get; set; }
            public string footer { get; set; }
        }
    }
}
