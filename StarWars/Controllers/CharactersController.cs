using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.DTO;
using StarWars.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRepository<Character> repository;

        public CharactersController(IRepository<Character> repository)
        {
            this.repository = repository;
        }

        // GET: api/Characters
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return (await repository.ReadAll()).Select(c => new CharacterDTO(c)).ToList();
        }

        // GET: api/Characters/Paged
        [Route("Paged")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharactersPaged(int pageSize = 5, int pageNumber = 0)
        {
            return (await repository.ReadAll())
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(c => new CharacterDTO(c)).ToList();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            CharacterDTO character = new CharacterDTO(await repository.Read(id));

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterDTO characterDTO)
        {
            Character character = new Character(characterDTO)
            {
                Id = id
            };

            try
            {
                await repository.Update(character);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> PostCharacter(CharacterDTO characterDTO)
        {
            Character character = new Character(characterDTO);

            CharacterDTO updatedCharacterDTO = new CharacterDTO(await repository.Create(character));

            return CreatedAtAction("GetCharacter", new { id = character.Id }, updatedCharacterDTO);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CharacterDTO>> DeleteCharacter(int id)
        {
            Character character = await repository.Delete(id);

            if (character == null)
            {
                return NotFound();
            }

            CharacterDTO characterDTO = new CharacterDTO(character);

            return characterDTO;
        }
    }
}
