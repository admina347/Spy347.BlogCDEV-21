using AutoMapper;

using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels.Account;

namespace Spy347.BlogCDEV_21.Web.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<LoginViewModel, User>();
            //CreateMap<LoginViewModel, User>()
            //    .ForMember(u => u.Email, opt => opt.MapFrom(l => l.Email));
            //CreateMap<User, UserWithFriendExt>();

            CreateMap<UserEditViewModel, User>();
            CreateMap<User, UserEditViewModel>().ForMember(x=>x.UserId, opt => opt.MapFrom(c => c.Id));

            //CreateMap<UserWithFriendExt, User>();
            //CreateMap<User, UserWithFriendExt>();

        }
    }
}
