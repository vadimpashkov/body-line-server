using AutoMapper;
using Domain.Abstractions.Queries;
using Domain.Entities;

namespace Domain.UseCases.Queries.Pictures
{
    public class PicturesViewModel: IFilter, IQueryOutput
    {
        public string Id { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
    }

    public class PicturesViewModelMapper: Profile 
    {
        public PicturesViewModelMapper()
        {
            CreateMap<Photos, PicturesViewModel>();
        }
    }
}