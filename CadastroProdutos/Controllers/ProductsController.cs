using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Library.Models.Request;
using CadastroProduto.Library.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.Api.Controllers
{
    /// <summary>
    /// Controller Products
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="mapper"></param>
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Search all registered products
        /// </summary>
        /// <response code="200">Products found</response>
        /// <response code="400">Some error happen with the request. This response could show the error's properties or just a message of what happen</response>
        /// <response code="401">Credentials found but without any app permissions configured</response>
        /// <param name="request">Represents model of requisition</param>
        /// <param name="ct">cancellation token, when triggered it cancels actions immediately</param>
        [ProducesResponseType(200, Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(400, Type = typeof(Exception))]
        [ProducesResponseType(404, Type = typeof(Exception))]
        [HttpPost("paginated")]
        public async Task<IActionResult> GetAllProductPaginated([FromBody] ProductPaginationRequest request, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                var pagedQueries = await _productService.GetAllProductsPaginatedAsync(request, ct);

                return Ok(new PaginationResponse<ProductResponse>
                {
                    PageIndex = pagedQueries.PageIndex,
                    TotalPages = pagedQueries.TotalPages,
                    TotalItems = pagedQueries.TotalItens,
                    Items = _mapper.Map<List<ProductResponse>>(pagedQueries)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Register product
        /// </summary>
        /// <response code="200">Product registred</response>
        /// <response code="400">Some error happen with the request. This response could show the error's properties or just a message of what happen</response>
        /// <response code="401">Credentials found but without any app permissions configured</response>
        /// <param name="request">Represents model of requisition</param>
        /// <param name="ct">cancellation token, when triggered it cancels actions immediately</param>
        [ProducesResponseType(200, Type = typeof(RequestResponse<ProductResponse>))]
        [ProducesResponseType(400, Type = typeof(Exception))]
        [ProducesResponseType(404, Type = typeof(Exception))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterProductAsync([FromForm] ProductRequest request, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                return Ok(new RequestResponse<ProductResponse> { Message = "Produto registrado com sucesso", Response = await _productService.CreateProductAsync(request, ct) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Register product
        /// </summary>
        /// <response code="200">Product registred</response>
        /// <response code="400">Some error happen with the request. This response could show the error's properties or just a message of what happen</response>
        /// <response code="401">Credentials found but without any app permissions configured</response>
        /// <param name="request">Represents model of requisition</param>
        /// <param name="ct">cancellation token, when triggered it cancels actions immediately</param>
        [ProducesResponseType(200, Type = typeof(RequestResponse))]
        [ProducesResponseType(400, Type = typeof(Exception))]
        [ProducesResponseType(404, Type = typeof(Exception))]
        [HttpPut]
        public async Task<IActionResult> EditProductAsync([FromForm] EditProductRequest request, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                await _productService.EditProductAsync(request, ct);

                return Ok(new RequestResponse { Message = "Produto registrado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <response code="200">Product deleted</response>
        /// <response code="400">Some error happen with the request. This response could show the error's properties or just a message of what happen</response>
        /// <response code="401">Credentials found but without any app permissions configured</response>
        /// <param name="productId">Product ID</param>
        /// <param name="ct">cancellation token, when triggered it cancels actions immediately</param>
        [ProducesResponseType(200, Type = typeof(RequestResponse))]
        [ProducesResponseType(404, Type = typeof(Exception))]
        [ProducesResponseType(400, Type = typeof(Exception))]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                await _productService.DeleteProductAsync(productId, ct);

                return Ok(new RequestResponse<ProductResponse> { Message = "Produto deletado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}