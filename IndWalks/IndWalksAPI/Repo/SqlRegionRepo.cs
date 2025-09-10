using IndWalksAPI.Data;
using IndWalksAPI.Models.DomainModels;
using IndWalksAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace IndWalksAPI.Repo
{
    public class SqlRegionRepo : IRegionRepo
    {
        private readonly INDWalksDbContext dbContext;

        public SqlRegionRepo(INDWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionModel = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionModel != null)
            {
                regionModel.Name = region.Name;
                regionModel.Code = region.Code;
                regionModel.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return region;
            }
            else
            {
                return null;
            }
        }
        public async Task<Region> DeleteAsync(Guid id)
        {
            var model = await dbContext.regions.FindAsync(id);
            if (model == null)
            {
                return null;
            }
            dbContext.regions.Remove(model);
            await dbContext.SaveChangesAsync();
            return model;
        }
    }
}
