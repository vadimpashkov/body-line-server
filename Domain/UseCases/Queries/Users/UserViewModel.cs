using System;
using AutoMapper;
using Domain.Abstractions.Queries;

namespace Domain.UseCases.Queries.Users
{
    public class UserViewModel: IFilter, IQueryOutput
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string UserName { get; set; }
        public string q { get; set; }
    }
    public class UserViewModelMapper: Profile
    {
        public UserViewModelMapper()
        {
            CreateMap<Entities.User, UserViewModel>();
        }
    }
}