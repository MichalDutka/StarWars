using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Data;
using StarWars.Model;

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
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            return (await repository.ReadAll()).ToList();
        }

        // GET: api/Characters/Paged
        [Route("Paged")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharactersPaged(int pageSize = 5, int pageNumber = 0)
        {
            return (await repository.ReadAll()).Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await repository.Read(id);

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
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            character.Id = id;

            try
            {
                await repository.Update(id, character);
            }
            catch (ArgumentException)
            {
                BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Exists(id))
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

        // POST: api/Characters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            await repository.Create(character);

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            var character = await repository.Delete(id);
            if (character == null)
            {
                return NotFound();
            }

            return character;
        }
    }
}
