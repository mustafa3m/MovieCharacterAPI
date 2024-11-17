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

    /// <summary>
    /// Gets all franchises.
    /// </summary>
    /// <returns>A list of franchises.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ReadFranchiseDto>>> GetFranchises()
    {
        var franchises = await _franchiseService.GetAllFranchisesAsync();
        return Ok(_mapper.Map<IEnumerable<ReadFranchiseDto>>(franchises));
    }

    /// <summary>
    /// Gets a specific franchise by ID.
    /// </summary>
    /// <param name="id">The ID of the franchise.</param>
    /// <returns>The requested franchise.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadFranchiseDto>> GetFranchise(int id)
    {
        var franchise = await _franchiseService.GetFranchiseByIdAsync(id);
        if (franchise == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadFranchiseDto>(franchise));
    }

    /// <summary>
    /// Creates a new franchise.
    /// </summary>
    /// <param name="createFranchiseDto">The franchise to create.</param>
    /// <returns>The created franchise.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadFranchiseDto>> CreateFranchise(CreateFranchiseDto createFranchiseDto)
    {
        var franchise = await _franchiseService.CreateFranchiseAsync(_mapper.Map<Franchise>(createFranchiseDto));
        return CreatedAtAction(nameof(GetFranchise), new { id = franchise.Id }, _mapper.Map<ReadFranchiseDto>(franchise));
    }

    /// <summary>
    /// Updates an existing franchise.
    /// </summary>
    /// <param name="id">The ID of the franchise to update.</param>
    /// <param name="updateFranchiseDto">The updated franchise information.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Deletes a franchise.
    /// </summary>
    /// <param name="id">The ID of the franchise to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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



