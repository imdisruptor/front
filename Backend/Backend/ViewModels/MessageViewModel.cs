using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class MessageViewModel
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        public string CatalogId { get; set; }
    }
}
