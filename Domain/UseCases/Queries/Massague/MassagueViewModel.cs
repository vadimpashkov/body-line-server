using AutoMapper;
using Domain.Abstractions.Queries;

namespace Domain.UseCases.Queries.Massague
{
    public class MassagueViewModel: IFilter, IQueryOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Occupation { get; set; }
        public string Description { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Src { get; set; }

        public string q { get; set; }
    }

    public class MassagueViewModelMapper: Profile 
    {
        public MassagueViewModelMapper()
        {
            CreateMap<Entities.Masseur, MassagueViewModel>()
                .ForMember(x => x.Src, 
                    map => map.MapFrom(
                        dest => dest.User.Src
                    ))
                .ForMember(x => x.UserName, 
                    map => map.MapFrom(
                        dest => dest.User.UserName
                    ))
                .ForMember(x => x.FirstName, 
                    map => map.MapFrom(
                        dest => dest.User.FirstName
                    ))
                .ForMember(x => x.LastName, 
                    map => map.MapFrom(
                        dest => dest.User.LastName
                    ));
        }
    }
}