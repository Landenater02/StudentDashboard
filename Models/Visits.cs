using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace StudentDashboard.Models
{
    public class Visits
    {
        [Key]
        public int Id { get; set; }

        public string Advisor { get; set; }

        public string Student { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        
        public Uri File { get; set; }

        public string ParentsCalled { get; set; }
        public string length { get; set; }

        public string[] Topics { get; set; }



    }
}
