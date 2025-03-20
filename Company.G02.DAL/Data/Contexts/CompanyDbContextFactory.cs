using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration.Json;
using System.IO;

using System.IO;

namespace Company.G02.DAL.Data.Contexts
{
    public class CompanyDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext CreateDbContext(string[] args)
        {
            // تحديد المسار الصحيح لمشروع API الذي يحتوي على appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Company.G02.PL");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // تعيين المسار الصحيح
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new CompanyDbContext(optionsBuilder.Options);
        }
    }
}
