using API.ViewModel;
using AutoMapper;
using Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_Start
{
    public class AutoMapperConfiguration
    {        
        public static void MapsConfig()
        {
            Mapper.Initialize(cfg =>
            {
                //Domain To Presentation
                cfg.CreateMap<Token, TokenViewModel>()
                    .ForMember(dest => dest.Id_Token, opt => opt.MapFrom(src => src.Id_Token.ToString()))
                    .ForMember(dest => dest.Id_Empresa, opt => opt.MapFrom(src => src.Id_Empresa.ToString()))
                    .ForMember(dest => dest.Data_Ativacao, opt => opt.MapFrom(src => src.Data_Ativacao.ToString()))
                    .ForMember(dest => dest.Data_Expiracao, opt => opt.MapFrom(src => src.Data_Expiracao.ToString()));

                cfg.CreateMap<Usuarios, UsuarioViewModel>()
                    .ForMember(dest => dest.Id_Token, opt => opt.MapFrom(src => src.Id_Token.ToString()))
                    .ForMember(dest => dest.Id_User, opt => opt.MapFrom(src => src.Id_User.ToString()))
                    .ForMember(dest => dest.IdUsuarioDv, opt => opt.MapFrom(src => src.IdUsuarioDv.ToString()));

                cfg.CreateMap<Empresa, EmpresaViewModel>()
                    .ForMember(dest => dest.Id_Empresa, opt => opt.MapFrom(src => src.Id_Empresa.ToString()))
                    .ForMember(dest => dest.Data_Cadastro, opt => opt.MapFrom(src => src.Data_Cadastro.ToString()))
                    .ForMember(dest => dest.Codigo_Empresa, opt => opt.MapFrom(src => src.Codigo_Empresa.ToString()));

                //Presentation To Domain
                cfg.CreateMap<TokenViewModel, Token>()
                    .ForMember(dest => dest.Id_Token, opt => opt.MapFrom(src => int.Parse(src.Id_Token)))
                    .ForMember(dest => dest.Id_Empresa, opt => opt.MapFrom(src => int.Parse(src.Id_Empresa)))
                    .ForMember(dest => dest.Data_Ativacao, opt => opt.MapFrom(src => Convert.ToDateTime(src.Data_Ativacao)))
                    .ForMember(dest => dest.Data_Expiracao, opt => opt.MapFrom(src => Convert.ToDateTime(src.Data_Expiracao)));

                cfg.CreateMap<UsuarioViewModel, Usuarios>()
                    .ForMember(dest => dest.Id_Token, opt => opt.MapFrom(src => int.Parse(src.Id_Token)))
                    .ForMember(dest => dest.Id_User, opt => opt.MapFrom(src => int.Parse(src.Id_User)))
                    .ForMember(dest => dest.IdUsuarioDv, opt => opt.MapFrom(src => int.Parse(src.IdUsuarioDv)));

                cfg.CreateMap<EmpresaViewModel, Empresa>()
                    .ForMember(dest => dest.Id_Empresa, opt => opt.MapFrom(src => int.Parse(src.Id_Empresa)))
                    .ForMember(dest => dest.Codigo_Empresa, opt => opt.MapFrom(src => int.Parse(src.Codigo_Empresa)))
                    .ForMember(dest => dest.Data_Cadastro, opt => opt.MapFrom(src => Convert.ToDateTime(src.Data_Cadastro)));
            });
        }        
    }
}