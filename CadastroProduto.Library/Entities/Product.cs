using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CadastroProduto.Library.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(1000)]
        public string UrlImage { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }
    }
}
