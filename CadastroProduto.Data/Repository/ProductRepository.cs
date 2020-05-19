using CadastroProduto.Data.Context;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Data.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(EntityContext context) : base(context) { }

        public async Task CreateProductAsync(Product product, CancellationToken ct)
        {
            try
            {
                using (var scope = CreateTransactionScopeWithIsolationLevel())
                {
                    await _context.Product.AddAsync(product, ct);

                    await _context.SaveChangesAsync(ct);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar produto", ex);
            }
        }

        public async Task<Product> DeleteProductAsync(Product product, CancellationToken ct)
        {
            using (var scope = CreateTransactionScopeWithIsolationLevel())
            {
                var productRegistred = await _context.Product.FirstOrDefaultAsync(x => x.ProductId.Equals(product.ProductId), ct);

                _context.Remove(productRegistred);

                await _context.SaveChangesAsync(ct);

                scope.Complete();

                return product;
            }
        }

        public async Task EditProductAsync(Product product, CancellationToken ct)
        {
            try
            {
                using (var scope = CreateTransactionScopeWithIsolationLevel())
                {
                    var productRegistred = await _context.Product.FirstOrDefaultAsync(x => x.ProductId.Equals(product.ProductId), ct);

                    productRegistred.Name = product.Name;
                    productRegistred.Price = product.Price;
                    productRegistred.UrlImage = product.UrlImage;
                    productRegistred.Updated = DateTime.UtcNow;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar produto", ex);
            }
        }

        public async Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(int pageIndex, int pageSize, string nameFilter, int price, CancellationToken ct)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produtos", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(Guid productId, CancellationToken ct)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(x => x.ProductId.Equals(productId), ct);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro so buscar produto", ex);
            }
        }
    }
}
