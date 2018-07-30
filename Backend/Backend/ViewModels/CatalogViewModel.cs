using Backend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class CatalogViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ParentCatalogId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MessageViewModel> MessageViewModel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogViewModel> Catalogs { get; set; }
    }
}
