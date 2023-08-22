using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PetController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var listPets = await _context.Pets.ToListAsync();
                var listPetsDTO = _mapper.Map<IEnumerable<PetDTO>>(listPets);

                return Ok(listPetsDTO);

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

                var petDTO = _mapper.Map<PetDTO>(pet);
                return Ok(petDTO);

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
        public async Task<IActionResult> Post(PetDTO petDTO)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petDTO);
                pet.FechaAlta = DateTime.Now;
                _context.Add(pet);
                await _context.SaveChangesAsync();

                var petDTO2 = _mapper.Map<PetDTO>(pet);
                return CreatedAtAction("Get", new { id = pet.Id }, petDTO2);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PetDTO petDTO)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petDTO);

                if (id != pet.Id)
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

                var petDTO2 = _mapper.Map<PetDTO>(pet);

                return NoContent();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
