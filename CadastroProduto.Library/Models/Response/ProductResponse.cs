using System;

namespace CadastroProduto.Library.Models.Response
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string UrlImage { get; set; }
    }
}
