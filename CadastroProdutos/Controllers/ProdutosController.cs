using System;
using System.Threading;
using System.Threading.Tasks;
using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Library.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.Api.Controllers
{
    /// <summary>
    /// Controller Products
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProductService _productService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService"></param>
        public ProdutosController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Search all registered programs
        /// </summary>
        /// <response code="200">Programs found</response>
        /// <response code="400">Some error happen with the request. This response could show the error's properties or just a message of what happen</response>
        /// <response code="401">Credentials found but without any app permissions configured</response>
        /// <param name="ct">cancellation token, when triggered it cancels actions immediately</param>
        [ProducesResponseType(200, Type = typeof(PaginationResponse<ProductResponse>) )]
        [ProducesResponseType(404, Type = typeof(Exception))]
        [HttpGet]
        public async Task<IActionResult> GetAllProductPaginated(CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}