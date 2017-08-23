using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SauliBlog.Models
{
    public class Fan
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Seniority { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}