using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Extensions;
using CadastroProduto.Library.Models;
using CadastroProduto.Library.Models.Request;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Business.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

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

        public async Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(ProductPaginationRequest request, CancellationToken ct)
        {
            request.Validate();

            request.pageIndex = request.pageIndex == 0 ? 1 : request.pageIndex;
            request.pageSize = request.pageSize == 0 ? 10 : request.pageSize;

            return await _productRepository.GetAllProductsPaginatedAsync(request.pageIndex, request.pageSize, request.nameFilter, request.price, ct);
        }
    }
}
