﻿using System;
using System.Collections.Generic;
using Data.Repositories;
using Data.Models;

namespace Logic
{
    public class UserLogic : IUser
    {
        private UserRepository repository;

        public UserLogic()
        {
            repository = new();
        }

        public IEnumerable<User> Get()
        {
            return repository.Get();
        }

        public User Get(int id)
        {
            return repository.Get(id);
        }

        public void Create(User user)
        {
            repository.Create(user);
        }

        public void Update(int id, User user)
        {
            repository.Update(id, user);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}