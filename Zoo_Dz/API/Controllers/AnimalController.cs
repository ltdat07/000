using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;
using Zoo_Dz.Application.Services;

namespace Zoo_Dz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IAnimalTransferService _transferService;

        public AnimalsController(IAnimalRepository animalRepo, IAnimalTransferService transferService)
        {
            _animalRepo = animalRepo;
            _transferService = transferService;
        }

        // GET: api/animals
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> GetAll()
        {
            return Ok(_animalRepo.GetAll());
        }

        // GET: api/animals/{id}
        [HttpGet("{id:guid}")]
        public ActionResult<Animal> GetById(Guid id)
        {
            try
            {
                var animal = _animalRepo.GetById(id);
                return Ok(animal);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/animals
        [HttpPost]
        public ActionResult<Animal> Create([FromBody] CreateAnimalDto dto)
        {
            var animal = new Animal(
                Guid.NewGuid(),
                dto.Species,
                dto.Name,
                dto.BirthDate,
                dto.Gender,
                dto.FavoriteFood,
                dto.EnclosureId
            );
            _animalRepo.Add(animal);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }

        // DELETE: api/animals/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _animalRepo.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/animals/{id}/transfer
        [HttpPost("{id:guid}/transfer")]
        public IActionResult Transfer(Guid id, [FromBody] TransferDto dto)
        {
            try
            {
                _transferService.TransferAnimal(id, dto.ToEnclosureId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CreateAnimalDto
    {
        public string Species { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Domain.ValueObjects.Gender Gender { get; set; }
        public Domain.ValueObjects.FoodType FavoriteFood { get; set; }
        public Guid EnclosureId { get; set; }
    }

    public class TransferDto
    {
        public Guid ToEnclosureId { get; set; }
    }
}
