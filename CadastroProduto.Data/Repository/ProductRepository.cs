using CadastroProduto.Data.Context;
using CadastroProduto.Data.Structure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Data.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(EntityContext context) : base(context) { }
    }
}
