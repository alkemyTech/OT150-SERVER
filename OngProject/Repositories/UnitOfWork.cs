using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngContext _context;
        private readonly IRepository<TestimonialsModel> _testimonialsModelRepository;
        private readonly IRepository<MemberModel> _memberModelRepository;
        private readonly IRepository<ActivityModel> _activityModelRepository;
        Feature/US-25
        private readonly IRepository<NewsModel> _newsModelRepository;

        private readonly IRepository<OrganizationModels> _organizationModelsRepository;
        private readonly IRepository<RoleModel> _roleModelRepository;
         dev

        public UnitOfWork(OngContext context)
        {
            _context = context;
        }

        public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
        public IRepository<MemberModel> MemberModelRepository => _memberModelRepository ?? new Repository<MemberModel>(_context);
        public IRepository<ActivityModel> ActivityModelRepository => _activityModelRepository ?? new Repository<ActivityModel>(_context);
        Feature/US-25
        public IRepository<NewsModel> NewsModelRepository => _newsModelRepository ?? new Repository<NewsModel>(_context);

        public IRepository<OrganizationModels> OrganizationModelsRepository => _organizationModelsRepository ?? new Repository<OrganizationModels>(_context);
        public IRepository<RoleModel> RoleModelRepository => _roleModelRepository ?? new Repository<RoleModel>(_context);
        dev

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
