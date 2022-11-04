using System.Data.Common;
using FarmerApp.Api.Services;
using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly ModelTypeService _modelTypeService;

    public ProductTypeController(ModelTypeService modelTypeService)
    {
        _modelTypeService = modelTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ModelTypeDTO>> Get()
    {
        try
        {
            return Ok(await _modelTypeService.GetAllAsync<ProductType>());
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