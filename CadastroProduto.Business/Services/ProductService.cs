using System;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CadastroProduto.Library.Models;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Extensions;
using CadastroProduto.Library.Models.Request;
using CadastroProduto.Library.Models.Response;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Business.Utils;

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

            var pathImage = await ManagerImage.SaveFileAsync(request.File);

            if (request.Price == 0)
            {
                throw new Exception("O preço do produto não pode ser R$0,00");
            }

            var product = request.ConvertToEntity();
            product.UrlImage = pathImage;
            product.Validate();

            await _productRepository.CreateProductAsync(product, ct);

            if (product.ProductId == Guid.Empty)
            {
                throw new Exception("Erro ao registrar produto");
            }

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task DeleteProductAsync(Guid productId, CancellationToken ct)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException("Id do produto não fornecido corretamente");
            }

            var product = await _productRepository.GetProductByIdAsync(productId, ct);

            if (product == null || product.ProductId == Guid.Empty)
            {
                throw new Exception("Produto não encotrado para o ID fornecido");
            }

            await _productRepository.DeleteProductAsync(productId, ct);

            ManagerImage.DeleteFile(product.UrlImage);
        }

        public async Task EditProductAsync(EditProductRequest request, CancellationToken ct)
        {
            request.Validate();

            if (request.Price == 0)
            {
                throw new Exception("O preço do produto não pode ser R$0,00");
            }

            var product = request.ConvertToEntity();
            var productregistred = await _productRepository.GetProductByIdAsync(product.ProductId, ct);

            if (productregistred == null || productregistred.ProductId == Guid.Empty)
            {
                throw new Exception("Produto não encotrado para o ID fornecido");
            }

            if (request.File != null)
            {
                var urlImage = await ManagerImage.SaveFileAsync(request.File);
                product.UrlImage = urlImage;
                ManagerImage.DeleteFile(productregistred.UrlImage);
            }
            else
            {
                product.UrlImage = productregistred.UrlImage;
            }

            await _productRepository.EditProductAsync(product, ct);
        }

        public async Task<PagedQueries<Product>> GetAllProductsPaginatedAsync(ProductPaginationRequest request, CancellationToken ct)
        {
            request.Validate();

            request.pageIndex = request.pageIndex == 0 ? 1 : request.pageIndex;
            request.pageSize = request.pageSize == 0 ? 10 : request.pageSize;

            var pagedQueries = await _productRepository.GetAllProductsPaginatedAsync(request.pageIndex, request.pageSize, request.nameFilter, request.price, ct);

            foreach (var product in pagedQueries)
            {
                product.UrlImage = await ManagerImage.GetFileAsync(product.UrlImage);
            }

            return pagedQueries;
        }
    }
}
