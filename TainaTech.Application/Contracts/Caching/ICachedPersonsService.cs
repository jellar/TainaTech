using System;
using System.Collections.Generic;
using System.Text;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Contracts.Caching
{
    public interface ICachedPersonsService
    {
        IEnumerable<Person> GetCachedPersons();
        void DeleteCachedPersons();
        void SetCachedPersons(IEnumerable<Person> entry);

    }
}
