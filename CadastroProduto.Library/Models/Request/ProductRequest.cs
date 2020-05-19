using CadastroProduto.Library.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProduto.Library.Models.Request
{
    public class ProductRequest
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do produto deve ser menor que 200 caracteres")]
        [MinLength(3, ErrorMessage = "O nome do produto deve ser maior que 3 caracteres")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo preço é obrigatório")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo imagem é obrigatório")]
        [MaxLength(1000, ErrorMessage = "O caminho da imagem é maior que do o permitido")]
        [MinLength(10, ErrorMessage = "O caminho da imagem deve ser maior que 10 caracteres")]
        public string UrlImage { get; set; }

        public virtual Product ConvertToEntity()
        {
            return new Product
            {
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
