using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Application.Dtos;
using RepositoryPattern.Application.Exceptions;
using RepositoryPattern.Application.Interfaces.Repositories;
using RepositoryPattern.Application.Wrappers;
using RepositoryPattern.Domain.Entities;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RepositoryPattern.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await _productRepository.GetByIdAsync(id);

            if (result is null) throw new ApiException($"Product Not Found.");

            var resulViewModel = _mapper.Map<GetProductByIdResponse>(result);

            return Ok(new ApiResponse<GetProductByIdResponse>(resulViewModel));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productRepository.GetAllAsync();

            var resulViewModelList = _mapper.Map<IEnumerable<GetAllProductsResponse>>(result);

            return Ok(new ApiResponse<IEnumerable<GetAllProductsResponse>>(resulViewModelList));
        }

        [HttpGet("GetPagedAll")]
        public async Task<IActionResult> GetPagedAllProducts([FromQuery] GetAllProductsRequest request)
        {
            var result = await _productRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);

            var resulViewModelList = _mapper.Map<IEnumerable<GetAllProductsResponse>>(result);

            return Ok(new PagedResponse<IEnumerable<GetAllProductsResponse>>(resulViewModelList, request.PageNumber, request.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductCreateRequest request)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null) throw new ApiException($"Product Not Found.");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;

            await _productRepository.UpdateAsync(product);

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null) throw new ApiException($"Product Not Found.");

            await _productRepository.DeleteAsync(product);

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }
    }
}
