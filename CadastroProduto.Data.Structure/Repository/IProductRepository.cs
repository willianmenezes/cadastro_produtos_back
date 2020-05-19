﻿using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Data.Structure.Repository
{
    public interface IProductRepository
    {
        Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(int pageIndex, int pageSize, string nameFilter, int price, CancellationToken ct);

        Task<Product> EditProductAsync(Product product, CancellationToken ct);

        Task<Product> CreateProductAsync(Product product, CancellationToken ct);

        Task<Product> DeleteProductAsync(Product product, CancellationToken ct);
    }
}