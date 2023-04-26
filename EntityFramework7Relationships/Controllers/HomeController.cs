using EntityFramework7Relationships.Data;
using EntityFramework7Relationships.Dtos;
using EntityFramework7Relationships.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework7Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public HomeController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _ctx.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .FirstOrDefaultAsync(c => c.Id == id);

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDto req)
        {
            var newCharacter = new Character
            {
                Name = req.Name,
            };

            var backpack = new Backpack
            {
                Description = req.Backpack.Description,
                Character = newCharacter
            };

            var weapons = req.Weapons.Select(x => new Weapon
            {
                Name=x.Name,
                Character = newCharacter
            })
                .ToList();

            var factions = req.Factions.Select(x => new Faction
            {
                Name = x.Name,
                Characters = new List<Character>
                {
                    newCharacter
                }
            })
                .ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _ctx.Characters.Add(newCharacter);
            await _ctx.SaveChangesAsync();

            return Ok(await 
                _ctx.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .ToListAsync()
                );
        }
    }
}

