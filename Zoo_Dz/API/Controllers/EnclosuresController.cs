using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosureRepo;

        public EnclosuresController(IEnclosureRepository enclosureRepo)
        {
            _enclosureRepo = enclosureRepo;
        }

        // GET: api/enclosures
        [HttpGet]
        public ActionResult<IEnumerable<Enclosure>> GetAll()
            => Ok(_enclosureRepo.GetAll());

        // GET: api/enclosures/{id}
        [HttpGet("{id:guid}")]
        public ActionResult<Enclosure> GetById(Guid id)
        {
            try
            {
                return Ok(_enclosureRepo.GetById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/enclosures
        [HttpPost]
        public ActionResult<Enclosure> Create([FromBody] CreateEnclosureDto dto)
        {
            var enclosure = new Enclosure(
                Guid.NewGuid(),
                dto.Type,
                dto.SizeSqM,
                dto.Capacity
            );
            _enclosureRepo.Add(enclosure);
            return CreatedAtAction(nameof(GetById), new { id = enclosure.Id }, enclosure);
        }

        // DELETE: api/enclosures/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _enclosureRepo.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }

    public class CreateEnclosureDto
    {
        public Domain.ValueObjects.EnclosureType Type { get; set; }
        public double SizeSqM { get; set; }
        public int Capacity { get; set; }
    }
}
