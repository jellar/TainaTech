using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Caching;
using TainaTech.Domain.Entities;

namespace TainaTech.App.Infrastructure.Services
{
    public class InMemoryCachedPersonsService : ICachedPersonsService
    {
        private readonly IMemoryCache _cache;
        private const string PersonsKey = nameof(PersonsKey);
        public InMemoryCachedPersonsService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public void DeleteCachedPersons()
        {
            _cache.Remove(PersonsKey);
        }

        public IEnumerable<Person> GetCachedPersons()
        {
            IEnumerable<Person> toDoItems;

            _cache.TryGetValue<IEnumerable<Person>>(PersonsKey, out toDoItems);

            return toDoItems;
        }

        public void SetCachedPersons(IEnumerable<Person> entry)
        {
            // Set cache options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(4));

            _cache.Set(PersonsKey, entry, cacheEntryOptions);
        }


    }
}
