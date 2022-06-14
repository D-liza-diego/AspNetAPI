using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspNetAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Salesdetails = new HashSet<Salesdetail>();
        }

        public int Idproduct { get; set; }
        public string Nameproduct { get; set; } = null!;
        public double? Precio { get; set; }
        public int? Idcategoria { get; set; }
        public int? Cantidad { get; set; }
        public virtual Categoria? IdcategoriaNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Salesdetail>? Salesdetails { get; set; }
    }
}
