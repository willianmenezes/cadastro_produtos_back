using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models;
using CadastroProduto.Library.Models.Request;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(ProductPaginationRequest request, CancellationToken ct);

        Task<Product> EditProductAsync(Product product, CancellationToken ct);

        Task<Product> CreateProductAsync(Product product, CancellationToken ct);

        Task<Product> DeleteProductAsync(Product product, CancellationToken ct);
    }
}
