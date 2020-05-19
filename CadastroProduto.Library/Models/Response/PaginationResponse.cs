using System.Collections.Generic;

namespace CadastroProduto.Library.Models.Response
{
    public class PaginationResponse<TEntity>
    {
        public int TotalPaginas { get; set; }

        public int IndicePagina { get; set; }

        public int TotalItens { get; set; }

        public List<TEntity> Itens { get; set; }
    }
}
