﻿using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
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

    public class VectorMap : ClassMap<Vector>
    {
        public VectorMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            References(x => x.VectorType);

        }
    }
}
