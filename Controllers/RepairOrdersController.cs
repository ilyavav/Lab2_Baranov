using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_Baranov.Models;
using Lab2_Baranov.Data;

[Route("api/[controller]")]
[ApiController]
public class RepairOrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public RepairOrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RepairOrder>>> GetRepairOrders()
    {
        return await _context.RepairOrders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RepairOrder>> GetRepairOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);

        if (repairOrder == null)
        {
            return NotFound();
        }

        return repairOrder;
    }

    [HttpGet("{id}/total-cost")]
    public async Task<IActionResult> GetTotalCost(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);

        if (repairOrder == null)
        {
            return NotFound();
        }

        var totalCost = repairOrder.GetTotalCost();

        return Ok(totalCost);
    }

    [Authorize]
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);

        if (repairOrder == null)
        {
            return NotFound();
        }

        repairOrder.CompleteOrder();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRepairOrder(int id, RepairOrder repairOrder)
    {
        if (id != repairOrder.Id)
        {
            return BadRequest();
        }

        _context.Entry(repairOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RepairOrderExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult<RepairOrder>> PostRepairOrder(RepairOrder repairOrder)
    {
        _context.RepairOrders.Add(repairOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRepairOrder", new { id = repairOrder.Id }, repairOrder);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepairOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);

        if (repairOrder == null)
        {
            return NotFound();
        }

        _context.RepairOrders.Remove(repairOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RepairOrderExists(int id)
    {
        return _context.RepairOrders.Any(e => e.Id == id);
    }
}