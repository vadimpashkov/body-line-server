﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Data;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Deleted { get; private set; }
        public string Src { get; private set; }
        
        public DateTime RegistrationDate { get; private set; }

        public List<UserRoleEntity> RolesEntities { get; private set; }
        public IReadOnlyList<UserRole> Roles => _roles ??= GetRoles();

        private List<UserRole> _roles;

        public User(DateTime dateCreated, string userName, string firstName, string lastName)
        {
            UserName = userName;
            SecurityStamp = Guid.NewGuid().ToString();
            RegistrationDate = dateCreated;
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetSrc(string src) 
        {
            Src = src;
        }

        public void Update(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected User() { }

        public User(DateTime Now, string AdminUserName)
        {
            this.RegistrationDate = Now;
            this.UserName = AdminUserName;
        }

        public bool IsStaff => Roles.Any(r => r == UserRole.Admin || r == UserRole.Employee);

        public void SetRegistrationDate(DateTime date)
        {
            RegistrationDate = date;
        }

        private List<UserRole> GetRoles() =>
            RolesEntities
                .Select(r => Enum.Parse<UserRole>(r.Role.Name))
                .ToList();

        public void Delete()
        {
            Deleted = true;

            var delGuid = Guid.NewGuid().ToString("N");

            PhoneNumber = delGuid;

            Email = delGuid;
            NormalizedEmail = delGuid;

            UserName = delGuid;
            NormalizedUserName = delGuid;
        }
    }
}