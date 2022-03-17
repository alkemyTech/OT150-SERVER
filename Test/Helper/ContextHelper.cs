using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;

namespace Test.Helper
{
    public class ContextHelper
    {
        public static OngContext DbContext { get; set; }
        public static IConfiguration configuration;
        public static IUnitOfWork unitOfWork;
        public static EntityMapper entityMapper;
        public static IJwtHelper jwtHelper;
        public static IHttpContextAccessor httpContext;
        public static ImagesBusiness imagesBusiness;

        public static void MakeContext()
        {
            entityMapper = new EntityMapper();
            configuration = new ConfigurationHelper().configuration;
            jwtHelper = new JwtHelper(configuration);
            httpContext = new HttpContextAccessor();

        }
    }
}
