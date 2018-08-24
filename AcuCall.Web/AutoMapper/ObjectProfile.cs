using AcuCall.Web.Models;
using AutoMapper;

namespace AcuCall.Web.AutoMapper
{
    public class ObjectProfile : Profile
    {
        public ObjectProfile()
        {
            CreateMap<Core.Objects.User, User>();
            CreateMap<Core.Objects.Report, Report>();
        }
    }
}
