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
        private readonly IRepository<NewsModel> _newsModelRepository;
        private readonly IRepository<OrganizationModel> _organizationModelRepository;
        private readonly IRepository<RoleModel> _roleModelRepository;
        private readonly IRepository<CategorieModel> _categorieModelRepository;
        private readonly IRepository<UserModel> _userModelRepository;

        public UnitOfWork(OngContext context)
        {
            _context = context;
        }

        public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
        public IRepository<MemberModel> MemberModelRepository => _memberModelRepository ?? new Repository<MemberModel>(_context);
        public IRepository<ActivityModel> ActivityModelRepository => _activityModelRepository ?? new Repository<ActivityModel>(_context);
        public IRepository<NewsModel> NewsModelRepository => _newsModelRepository ?? new Repository<NewsModel>(_context);
        public IRepository<OrganizationModel> OrganizationModelRepository => _organizationModelRepository ?? new Repository<OrganizationModel>(_context);
        public IRepository<RoleModel> RoleModelRepository => _roleModelRepository ?? new Repository<RoleModel>(_context);
        public IRepository<CategorieModel> CategorieModelRepository => _categorieModelRepository ?? new Repository<CategorieModel>(_context);
        public IRepository<UserModel> UserModelRepository => _userModelRepository ?? new Repository<UserModel>(_context);

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
