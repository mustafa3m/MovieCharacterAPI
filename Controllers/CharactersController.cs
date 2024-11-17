using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieCharacterAPI.Services;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.DTO.CharacterDTOs;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;
    private readonly IMapper _mapper;

    public CharactersController(ICharacterService characterService, IMapper mapper)
    {
        _characterService = characterService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all the characters.
    /// </summary>
    /// <returns>A list of characters.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadCharacterDto>>> GetCharacters()
    {
        var characters = await _characterService.GetAllCharactersAsync();
        return Ok(_mapper.Map<IEnumerable<ReadCharacterDto>>(characters));
    }

  
    /// <summary>
    /// Gets a specific character by ID.
    /// </summary>
    /// <param name="id">The ID of the character.</param>
    /// <returns>The requested character.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadCharacterDto>> GetCharacter(int id)
    {
        var character = await _characterService.GetCharacterByIdAsync(id);
        if (character == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadCharacterDto>(character));
    }

    
    /// <summary>
    /// Creates a new character.
    /// </summary>
    /// <param name="createCharacterDto">The character to create.</param>
    /// <returns>The created character.</returns>
    [HttpPost]
    public async Task<ActionResult<ReadCharacterDto>> CreateCharacter(CreateCharacterDto createCharacterDto)
    {
        var character = await _characterService.CreateCharacterAsync(_mapper.Map<Character>(createCharacterDto));
        return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, _mapper.Map<ReadCharacterDto>(character));
    }

    
    /// <summary>
    /// Updates an existing character.
    /// </summary>
    /// <param name="id">The ID of the character to update.</param>
    /// <param name="updateCharacterDto">The updated character information.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(int id, UpdateCharacterDto updateCharacterDto)
    {
        if (id != updateCharacterDto.Id)
        {
            return BadRequest();
        }

        var character = await _characterService.UpdateCharacterAsync(_mapper.Map<Character>(updateCharacterDto));
        if (character == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    
    /// <summary>
    /// Deletes a character.
    /// </summary>
    /// <param name="id">The ID of the character to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var result = await _characterService.DeleteCharacterAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}