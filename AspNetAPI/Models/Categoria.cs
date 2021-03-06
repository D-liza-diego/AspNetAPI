using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspNetAPI.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Products = new HashSet<Product>();
        }

        public int Idcategoria { get; set; }
        public string Catname { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
