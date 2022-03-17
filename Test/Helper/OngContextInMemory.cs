using Microsoft.EntityFrameworkCore;

namespace Test.Helper
{
    public class OngContextInMemory : DbContext
    {
        public OngContextInMemory(DbContextOptions<OngContextInMemory> options) : base(options)
        {

        }
    }
}
