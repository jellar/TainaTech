using AutoMapper;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonListVm>().ReverseMap();         
        }
    }
}
