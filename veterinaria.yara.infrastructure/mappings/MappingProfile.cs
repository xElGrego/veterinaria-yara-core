using AutoMapper;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Mascota;
using veterinaria.yara.domain.DTOs.Raza;
using veterinaria.yara.domain.entities;

namespace veterinaria.yara.infrastructure.mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Mascota, MascotaDTO>();
            CreateMap<EditarMascotaDTO, Mascota>();



            CreateMap<NuevaMascotaDto, Mascota>()
              .ForMember(x => x.IdMascota, d => d.MapFrom(model => Guid.NewGuid()))
              .ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now))
              .ForMember(x => x.Estado, d => d.MapFrom(model => true));


            CreateMap<Raza, RazaDTO>().ReverseMap();

            CreateMap<NuevaRazaDTO, Raza>()
            .ForMember(x => x.IdRaza, d => d.MapFrom(model => Guid.NewGuid()))
            .ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now));


            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(x => x.IdUsuario, d => d.MapFrom(model => Guid.NewGuid()))
            .ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now))
            .ForMember(x => x.Clave, d => d.MapFrom(model => model.Clave))
            .ForMember(x => x.Estado, d => d.MapFrom(model => true));

            CreateMap<MensajeDTO, Mensaje>();


        }
    }
}
