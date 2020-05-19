using AutoMapper;
using CadastroProduto.Library.Entities;
using CadastroProduto.Library.Models.Response;

namespace CadastroProduto.Library.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
