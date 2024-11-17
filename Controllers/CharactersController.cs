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

    // Get all characters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadCharacterDto>>> GetCharacters()
    {
        var characters = await _characterService.GetAllCharactersAsync();
        return Ok(_mapper.Map<IEnumerable<ReadCharacterDto>>(characters));
    }

    // Get a specific character by ID
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

    // Create a new character
    [HttpPost]
    public async Task<ActionResult<ReadCharacterDto>> CreateCharacter(CreateCharacterDto createCharacterDto)
    {
        var character = await _characterService.CreateCharacterAsync(_mapper.Map<Character>(createCharacterDto));
        return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, _mapper.Map<ReadCharacterDto>(character));
    }

    // Update an existing character
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

    // Delete a character
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