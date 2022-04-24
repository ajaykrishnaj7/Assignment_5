using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataGov_API_Intro.Models {
       public class DNCComplaints
    {
        public string id { get; set; }
        public Attributes attributes { get; set; }
    }


    public class D
    {
        public List<DNCComplaints> data { get; set; }
    }
    public class Attributes
    {
        // public int id { get; set; }
        [Key]
        public string transaction_number { get; set; }

        public string title { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string acquiredparty { get; set; }
        public string acquiringparty { get; set; }

        // public List<string> Acquired_entities { get; set; }
        

        public string date { get; set; }


    }
}
