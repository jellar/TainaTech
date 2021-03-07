using AutoMapper;
using TainaTech.Application.Features.Persons.Commands.CreatePerson;
using TainaTech.Application.Features.Persons.Commands.UpdatePerson;
using TainaTech.Application.Features.Persons.Queries.GetPerson;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonListVm>().ReverseMap();
            CreateMap<Person, PersonDetailsVm>().ReverseMap();
            CreateMap<Person, CreatePersonCommand>().ReverseMap();
            CreateMap<Person, UpdatePersonCommand>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
