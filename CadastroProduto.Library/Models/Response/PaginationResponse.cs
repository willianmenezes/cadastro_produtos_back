using System.Collections.Generic;

namespace CadastroProduto.Library.Models.Response
{
    public class PaginationResponse<TEntity>
    {
        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public int TotalItems { get; set; }

        public List<TEntity> Items { get; set; }
    }
}
