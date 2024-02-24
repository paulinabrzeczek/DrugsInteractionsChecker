using AutoMapper;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.ViewModels;

namespace InterakcjeMiedzyLekami.Configuration.Mapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Pharmaceutical, PharmaceuticalsSubstancesVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPharmaceutical))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NamePharmaceutical))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Pharmaceutical"));

            CreateMap<OtherSubstance, PharmaceuticalsSubstancesVM>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdSubstance))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameSubstance))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Substance"));
        }
    }
}
