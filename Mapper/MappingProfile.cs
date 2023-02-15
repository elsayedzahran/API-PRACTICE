using AutoMapper;

namespace API_Practice.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Medicine, MedicinesDetailsDto>();

            CreateMap<MedicineDto, Medicine>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
