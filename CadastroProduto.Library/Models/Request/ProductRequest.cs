using CadastroProduto.Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProduto.Library.Models.Request
{
    public class ProductRequest
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do produto deve ser menor que 200 caracteres")]
        [MinLength(3, ErrorMessage = "O nome do produto deve ser maior que 3 caracteres")]
        [FromForm(Name = "name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo preço é obrigatório")]
        [FromForm(Name = "price")]
        public double Price { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

        public virtual Product ConvertToEntity()
        {
            return new Product
            {
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Status = true,
                Name = Name.Trim(),
                Price = Price / 100.0
            };
        }
    }
}
