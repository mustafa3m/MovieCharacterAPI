using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieCharacterAPI.Services;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.DTO.FranchiseDTOs;

[ApiController]
[Route("api/[controller]")]
public class FranchisesController : ControllerBase
{
    private readonly IFranchiseService _franchiseService;
    private readonly IMapper _mapper;

    public FranchisesController(IFranchiseService franchiseService, IMapper mapper)
    {
        _franchiseService = franchiseService;
        _mapper = mapper;
    }

    // Get all franchises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadFranchiseDto>>> GetFranchises()
    {
        var franchises = await _franchiseService.GetAllFranchisesAsync();
        return Ok(_mapper.Map<IEnumerable<ReadFranchiseDto>>(franchises));
    }

    // Get a specific franchise by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadFranchiseDto>> GetFranchise(int id)
    {
        var franchise = await _franchiseService.GetFranchiseByIdAsync(id);
        if (franchise == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadFranchiseDto>(franchise));
    }

    // Create a new franchise
    [HttpPost]
    public async Task<ActionResult<ReadFranchiseDto>> CreateFranchise(CreateFranchiseDto createFranchiseDto)
    {
        var franchise = await _franchiseService.CreateFranchiseAsync(_mapper.Map<Franchise>(createFranchiseDto));
        return CreatedAtAction(nameof(GetFranchise), new { id = franchise.Id }, _mapper.Map<ReadFranchiseDto>(franchise));
    }

    // Update an existing franchise
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFranchise(int id, UpdateFranchiseDto updateFranchiseDto)
    {
        if (id != updateFranchiseDto.Id)
        {
            return BadRequest();
        }

        var franchise = await _franchiseService.UpdateFranchiseAsync(_mapper.Map<Franchise>(updateFranchiseDto));
        if (franchise == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Delete a franchise
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFranchise(int id)
    {
        var result = await _franchiseService.DeleteFranchiseAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}