using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCDArch.Core.DomainModel;

namespace CAESGenome.Core.Domain
{
    public class Vector : DomainObject
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        public virtual VectorType VectorType { get; set; }
    }
}
