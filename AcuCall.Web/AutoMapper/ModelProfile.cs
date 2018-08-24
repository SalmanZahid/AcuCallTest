using AcuCall.Core.Objects;
using AutoMapper;

namespace AcuCall.Web.AutoMapper
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Models.User, User>();            
        }
    }
}
