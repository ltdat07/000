using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;
using Zoo_Dz.Application.Services;
using System.Runtime.InteropServices;

namespace Zoo_Dz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _scheduleRepo;
        private readonly IFeedingOrganizationService _feedingService;

        public FeedingSchedulesController(
            IFeedingScheduleRepository scheduleRepo,
            IFeedingOrganizationService feedingService)
        {
            _scheduleRepo = scheduleRepo;
            _feedingService = feedingService;
        }

        // GET: api/feedingschedules
        [HttpGet]
        public ActionResult<IEnumerable<FeedingSchedule>> GetAll()
            => Ok(_scheduleRepo.GetAll());

        // GET: api/feedingschedules/{id}
        [HttpGet("{id:guid}")]
        public ActionResult<FeedingSchedule> GetById(Guid id)
        {
            try
            {
                return Ok(_scheduleRepo.GetById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/feedingschedules
        [HttpPost]
        public ActionResult<FeedingSchedule> Create([FromBody] CreateScheduleDto dto)
        {
            var schedule = new FeedingSchedule(
                Guid.NewGuid(),
                dto.AnimalId,
                dto.ScheduledTime,
                dto.FoodType
            );
            _scheduleRepo.Add(schedule);
            return CreatedAtAction(nameof(GetById), new { id = schedule.Id }, schedule);
        }

        // PUT: api/feedingschedules/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Reschedule(Guid id, [FromBody] RescheduleDto dto)
        {
            try
            {
                _feedingService.RescheduleFeeding(id, dto.NewTime);
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

        // POST: api/feedingschedules/{id}/perform
        [HttpPost("{id:guid}/perform")]
        public IActionResult Perform(Guid id)
        {
            try
            {
                _feedingService.PerformFeeding(id);
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

    public class CreateScheduleDto
    {
        public Guid AnimalId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public Domain.ValueObjects.FoodType FoodType { get; set; }
    }

    public class RescheduleDto
    {
        public DateTime NewTime { get; set; }
    }
}
