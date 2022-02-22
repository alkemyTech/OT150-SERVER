using OngProject.Entities;
using System;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<TestimonialsModel> TestimonialsModelRepository { get; }
        IRepository<MemberModel> MemberModelRepository { get; }
        IRepository<ActivityModel> ActivityModelRepository { get; }
        IRepository<OrganizationModels> OrganizationModelsRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
