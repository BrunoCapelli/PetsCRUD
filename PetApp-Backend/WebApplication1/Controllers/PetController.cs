using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private ApplicationDbContext _context;
        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var listPets = await _context.Pets.ToListAsync();
                return Ok(listPets);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);

                if(pet == null )
                {
                    return NotFound();
                }
                return Ok(pet);


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
            var pet = await _context.Pets.FindAsync(id);
            if(pet == null)
            {
                return NotFound();
            }
            else
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
                return Ok();
            }


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pet pet)
        {
            try
            {
                pet.FechaAlta = DateTime.Now;
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = pet.Id }, pet);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Pet pet)
        {
            try
            {
                if(id != pet.Id)
                {
                    return BadRequest();
                }

                var petItem = await _context.Pets.FindAsync(id);

                if (petItem == null)
                {
                    return NotFound();
                }

                petItem.Nombre = pet.Nombre;
                petItem.Raza = pet.Raza;
                petItem.Color = pet.Color;
                petItem.Peso = pet.Peso;

                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
