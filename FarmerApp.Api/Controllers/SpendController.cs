using FarmerApp.Api.Services;
using FarmerApp.Data.DTO;
using FarmerApp.Data.ViewModels.Spending;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SpendController : Controller
{
    private readonly SpendService _spendService;

    public SpendController(SpendService spendService)
    {
        _spendService = spendService;
    }

    [HttpPost("own")]
    public async Task<IActionResult> AddOwn(SpendOwnVM own)
    {
        try
        {
            return Ok(await _spendService.AddAsync(own));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("fromWarehouse")]
    public async Task<IActionResult> AddFromWarehouse(SpendFromWarehouseVM fromWarehouse)
    {
        try
        {
            return Ok(await _spendService.AddAsync(fromWarehouse));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("salary")]
    public async Task<IActionResult> AddSalary(SpendSalaryVM salary)
    {
        try
        {
            return Ok(await _spendService.AddAsync(salary));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("location/{id}")]
    public async Task<ActionResult<LocationSpendDTO>> GetSpending(int id)
    {
        try
        {
            return Ok(await _spendService.GetLocationSpendAsync(id));
        }
        catch (Exception e)
        {
            BadRequest(e);
            throw;
        }
    }
}