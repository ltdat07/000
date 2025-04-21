using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Infrastructure.Repositories
{
    public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly ConcurrentDictionary<Guid, FeedingSchedule> _storage = new();

        public FeedingSchedule GetById(Guid id)
        {
            if (!_storage.TryGetValue(id, out var schedule))
                throw new KeyNotFoundException($"FeedingSchedule with Id = {id} not found.");

            return schedule;
        }

        public IEnumerable<FeedingSchedule> GetAll() => _storage.Values.ToList();

        public void Add(FeedingSchedule schedule)
        {
            if (!_storage.TryAdd(schedule.Id, schedule))
                throw new InvalidOperationException($"FeedingSchedule with Id = {schedule.Id} already exists.");
        }

        public void Remove(Guid id)
        {
            if (!_storage.TryRemove(id, out _))
                throw new KeyNotFoundException($"FeedingSchedule with Id = {id} not found.");
        }
    }
}
