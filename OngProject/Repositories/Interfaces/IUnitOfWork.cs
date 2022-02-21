﻿using OngProject.Entities;
using System;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<TestimonialsModel> TestimonialsModelRepository { get; }
        IRepository<ActivityModel> ActivityModelRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
