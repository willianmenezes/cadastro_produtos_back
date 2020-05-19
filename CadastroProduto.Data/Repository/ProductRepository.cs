﻿using CadastroProduto.Data.Context;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Data.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(EntityContext context) : base(context) { }

        public Task<Product> CreateProductAsync(Product product, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteProductAsync(Product product, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Product> EditProductAsync(Product product, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(int pageIndex, int pageSize, string nameFilter, int price, CancellationToken ct)
        {
            var querie = _context.Product.AsNoTracking();

            if (price > 0)
            {
                querie = querie.Where(x => x.Price > price).AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                querie = querie.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{nameFilter.ToLower()}%")).AsNoTracking();
            }

            var pagedQueries = await PagedQueries<Product>.CreateAsync(querie, pageIndex, pageSize, ct);

            return pagedQueries;
        }
    }
}