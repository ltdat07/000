using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Infrastructure.Repositories
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly ConcurrentDictionary<Guid, Animal> _storage = new();

        public Animal GetById(Guid id)
        {
            if (!_storage.TryGetValue(id, out var animal))
                throw new KeyNotFoundException($"Animal with Id = {id} not found.");

            return animal;
        }

        public IEnumerable<Animal> GetAll() => _storage.Values.ToList();

        public void Add(Animal animal)
        {
            if (!_storage.TryAdd(animal.Id, animal))
                throw new InvalidOperationException($"Animal with Id = {animal.Id} already exists.");
        }

        public void Remove(Guid id)
        {
            if (!_storage.TryRemove(id, out _))
                throw new KeyNotFoundException($"Animal with Id = {id} not found.");
        }
    }
}
