using CadastroProduto.Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CadastroProduto.Library.Models.Request
{
    public class EditProductRequest : ProductRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo ID é obrigatório")]
        public Guid ProductId { get; set; }

        public override Product ConvertToEntity()
        {
            return new Product
            {
                ProductId = ProductId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Status = true,
                Name = Name.Trim(),
                Price = Price,
                UrlImage = UrlImage.Trim()
            };
        }
    }
}
