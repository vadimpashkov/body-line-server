using System;
using AutoMapper;
using Domain.Abstractions.Queries;

namespace Domain.UseCases.Queries.Consult
{
    public class ConsultViewModel: IFilter, IQueryOutput
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreation { get; set; }
    }

    public class ConsultViewModelMapper: Profile 
    {
        public ConsultViewModelMapper()
        {
            CreateMap<Entities.Consultation, ConsultViewModel>();
        }
    }
}