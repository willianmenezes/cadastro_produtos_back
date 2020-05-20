using System.ComponentModel.DataAnnotations;

namespace CadastroProduto.Library.Models.Request
{
    public class ProductPaginationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O índice da página é obrigatório")]
        public int pageIndex { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O tamanho da pagina é obrigatório")]
        public int pageSize { get; set; }

        public string nameFilter { get; set; }

        public int price { get; set; }
    }
}
