using AutoMapper;
using Backend.Models;
using Backend.Models.Entities;
using Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Mappings
{
    public class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(rvm => rvm.Email));
            CreateMap<CatalogViewModel, Catalog>()
                .ForMember(c => c.ChildCatalogs, map => map.MapFrom(cvm => cvm.Catalogs)).
                ForMember(c => c.Messages, map => map.MapFrom(cvm => cvm.MessageViewModel));
            CreateMap<MessageViewModel, Message>();
        }
    }

    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Catalog, CatalogViewModel>()
                .ForMember(cvm => cvm.Catalogs, map => map.MapFrom(c => c.ChildCatalogs))
                .ForMember(cvm => cvm.MessageViewModel, map => map.MapFrom(c => c.Messages));
            CreateMap<Message, MessageViewModel>();
        }
    }
}
