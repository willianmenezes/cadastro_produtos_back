using AutoMapper;
using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Extensions;
using CadastroProduto.Library.Models;
using CadastroProduto.Library.Models.Request;
using CadastroProduto.Library.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Business.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request, CancellationToken ct)
        {
            request.Validate();

            if (request.Price == 0)
            {
                throw new Exception("O preço do produto não pode ser R$0,00");
            }

            var product = request.ConvertToEntity();
            product.Validate();

            await _productRepository.CreateProductAsync(product, ct);

            if (product.ProductId == Guid.Empty)
            {
                throw new Exception("Erro ao registrar produto");
            }

            return _mapper.Map<ProductResponse>(product);
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
