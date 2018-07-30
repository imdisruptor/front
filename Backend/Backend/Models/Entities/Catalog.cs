using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Entities
{
    public class Catalog : EntityBase
    {
        public string Title { get; set; }

        public List<Message> Messages { get; set; }

        public string ParentCatalogId { get; set; }
        public Catalog ParentCatalog { get; set; }

        public List<Catalog> ChildCatalogs { get; set; }

        public Catalog()
        {
            ChildCatalogs = new List<Catalog>();
            Messages = new List<Message>();
        }
    }
}
