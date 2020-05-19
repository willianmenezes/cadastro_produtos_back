using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models;
using CadastroProduto.Library.Models.Request;
using CadastroProduto.Library.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(ProductPaginationRequest request, CancellationToken ct);

        Task<Product> EditProductAsync(Product product, CancellationToken ct);

        Task<ProductResponse> CreateProductAsync(ProductRequest product, CancellationToken ct);

        Task<Product> DeleteProductAsync(Product product, CancellationToken ct);
    }
}
