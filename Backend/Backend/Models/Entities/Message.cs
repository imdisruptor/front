using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Entities
{
    public class Message : EntityBase
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }
}
