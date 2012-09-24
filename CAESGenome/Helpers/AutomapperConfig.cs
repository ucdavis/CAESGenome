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
        }
    }
}