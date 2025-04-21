using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Infrastructure.Repositories
{
    public class InMemoryEnclosureRepository : IEnclosureRepository
    {
        private readonly ConcurrentDictionary<Guid, Enclosure> _storage = new();

        public Enclosure GetById(Guid id)
        {
            if (!_storage.TryGetValue(id, out var enclosure))
                throw new KeyNotFoundException($"Enclosure with Id = {id} not found.");

            return enclosure;
        }

        public IEnumerable<Enclosure> GetAll() => _storage.Values.ToList();

        public void Add(Enclosure enclosure)
        {
            if (!_storage.TryAdd(enclosure.Id, enclosure))
                throw new InvalidOperationException($"Enclosure with Id = {enclosure.Id} already exists.");
        }

        public void Remove(Guid id)
        {
            if (!_storage.TryRemove(id, out _))
                throw new KeyNotFoundException($"Enclosure with Id = {id} not found.");
        }
    }
}
