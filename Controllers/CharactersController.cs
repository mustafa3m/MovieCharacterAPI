using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieCharacterAPI.Services;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.DTO.CharacterDTOs;
using Microsoft.EntityFrameworkCore;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ReadCharacterDto>>> GetCharacters()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            return Ok(_mapper.Map<List<ReadCharacterDto>>(characters));
        }

        /// <summary>
        /// Gets a specific character by ID.
        /// </summary>
        /// <param name="id">The ID of the character.</param>
        /// <returns>The requested character.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCharacter(int id, UpdateCharacterDto updateCharacterDto)
        {
            if (id != updateCharacterDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var character = await _characterService.UpdateCharacterAsync(_mapper.Map<Character>(updateCharacterDto));
                if (character == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _characterService.CharacterExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a character.
        /// </summary>
        /// <param name="id">The ID of the character to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!await _characterService.CharacterExistsAsync(id))
            {
                return NotFound();
            }

            await _characterService.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}

