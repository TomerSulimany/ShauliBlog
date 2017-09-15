using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Fan : IEquatable<Fan>
    {
        static int count = 0;

        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Required, Range(0, 100)]
        [DataType(DataType.Text)]
        [Display(Name = "Seniority")]
        public int Seniority { get; set; }

        public Fan()
        {
            count++;
            ID = count;
        }

        public void copy(Fan fan)
        {
            ID = fan.ID;
            Name = fan.Name;
            Surname = fan.Surname;
            Gender = fan.Gender;
            Birthday = fan.Birthday;
            Seniority = fan.Seniority;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Fan);
        }

        public bool Equals(Fan other)
        {
            if (other == null)
                return false;

            return (this.ID == other.ID);
        }
    }

    public enum Gender
    {
        Male, Female
    }
}