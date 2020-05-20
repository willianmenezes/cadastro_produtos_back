using CadastroProduto.Library.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProduto.Library.Models.Request
{
    public class EditProductRequest : ProductRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo ID é obrigatório")]
        [FromForm(Name = "productId")]
        public Guid ProductId { get; set; }

        public override Product ConvertToEntity()
        {
            return new Product
            {
                ProductId = ProductId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                UrlImage = string.Empty,
                Status = true,
                Name = Name.Trim(),
                Price = Price / 100.0
            };
        }
    }
}
