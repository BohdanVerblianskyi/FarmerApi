using System.Data.Common;
using FarmerApp.Api.Services;
using FarmerApp.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        try
        {
            var all = await _productService.Get();
            return Ok(all);
        }
        catch (DbException e)
        {
            return BadRequest(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("byType/{id}")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllByType(int id)
    {
        try
        {
            var productDtos = await _productService.GetByType(id);
            return Ok(productDtos);
        }
        catch (DbException e)
        {
            return BadRequest(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        try
        {
            var byId = await _productService.Get(id);
            return Ok(byId);
        }
        catch (DbException e)
        {
            return BadRequest(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}