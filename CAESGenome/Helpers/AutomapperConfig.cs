using AutoMapper;
using CAESGenome.Core.Domain;
using CAESGenome.Models;

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

            CreateMap<SequencingPostModel, UserJob>()
                //.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                //.ForMember(dest => dest.RechargeAccount, opt => opt.MapFrom(src => src.RechargeAccount))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType))
                .ForMember(dest => dest.NumberPlates, opt => opt.MapFrom(src => src.NumPlates))
                .ForMember(dest => dest.PlateType, opt => opt.MapFrom(src => src.PlateType))
                .ForMember(x => x.UserJobPlates, x => x.Ignore())
                ;

            CreateMap<SequencingPostModel, UserJobBacterialClone>()
                .ForMember(dest => dest.SequenceDirection, opt => opt.MapFrom(src => src.SequenceDirection))
                .ForMember(dest => dest.Primer1, opt => opt.MapFrom(src => src.Primer1))
                .ForMember(dest => dest.Primer2, opt => opt.MapFrom(src => src.Primer2))
                .ForMember(dest => dest.Strain, opt => opt.MapFrom(src => src.Strain))
                .ForMember(dest => dest.Vector, opt => opt.MapFrom(src => src.Vector))
                .ForMember(dest => dest.Antibiotic, opt => opt.MapFrom(src => src.Antibiotic))
                ;

        }
    }
}