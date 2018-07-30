using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public string Id { get; set; }
    }
}
