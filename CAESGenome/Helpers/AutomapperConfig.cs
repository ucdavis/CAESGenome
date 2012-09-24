using AutoMapper;
using CAESGenome.Core.Domain;

namespace CAESGenome.Helpers
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ViewModelProfile>());
        }
    }

    public class ViewModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RechargeAccount, RechargeAccount>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.User, x => x.Ignore());

            CreateMap<User, User>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.ParentUser, x => x.Ignore())
                .ForMember(x => x.RechargeAccounts, x => x.Ignore())
                .ForMember(x => x.OwnedRechargeAcccounts, x => x.Ignore());

        }
    }
}