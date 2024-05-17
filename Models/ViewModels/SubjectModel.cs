using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mindafy.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    public class SubjectModel

    {
        public SubjectModel()
        {
            this.Student = new HashSet<Conforms>();
            this.Establishes = new HashSet<Establishes>();
        }
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [StringLength (100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Average")]
        public Nullable<double> Average { get; set; }
  
        [Display(Name = "Student")]
        public virtual ICollection<Establishes> Establishes { get; set; }

        public virtual ICollection<Conforms> Student{ get; set; }

    }
}