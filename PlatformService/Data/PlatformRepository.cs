using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _appDbContext;

        public PlatformRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreatePlatformAsync(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            await _appDbContext.Platforms.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _appDbContext.Platforms.ToListAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(Guid id)
        {
            return await _appDbContext.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
