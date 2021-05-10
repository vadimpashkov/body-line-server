using AutoMapper;
using Domain.Abstractions.Queries;

namespace Domain.UseCases.Queries.MassageType
{
    public class MassageTypeViewModel: IFilter, IQueryOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

    public class MassageTypeViewModelMapper: Profile 
    {
        public MassageTypeViewModelMapper()
        {
            CreateMap<Entities.MassageType, MassageTypeViewModel>();
        }
    }
}