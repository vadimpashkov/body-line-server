using System;
using AutoMapper;
using Domain.Abstractions.Queries;

namespace Domain.UseCases.Queries.Record
{
    public class RecordViewModel: IFilter, IQueryOutput
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public int MesseurId { get; set; }
        public int MassageTypeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecordViewModelMapper: Profile 
    {
        public RecordViewModelMapper()
        {
            CreateMap<Entities.Record, RecordViewModel>();
        }
    }
}