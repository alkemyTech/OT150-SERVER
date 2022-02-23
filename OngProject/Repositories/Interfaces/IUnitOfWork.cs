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
        IRepository<NewsModel> NewsModelRepository { get; }
        IRepository<OrganizationModels> OrganizationModelsRepository { get; }
        IRepository<RoleModel> RoleModelRepository { get; }
        IRepository<CategorieModel> CategorieModelRepository { get; }
        IRepository<UserModel> UserModelRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
