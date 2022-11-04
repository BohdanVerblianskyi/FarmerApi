using System.Data.Common;
using FarmerApp.Api.Services;
using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementUnitsController : ControllerBase
{
    private readonly ModelTypeService _modelTypeService;

    public MeasurementUnitsController(ModelTypeService modelTypeService)
    {
        _modelTypeService = modelTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ModelTypeDTO>> Get()
    {
        try
        {
            return Ok(await _modelTypeService.GetAllAsync<MeasurementUnit>());
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