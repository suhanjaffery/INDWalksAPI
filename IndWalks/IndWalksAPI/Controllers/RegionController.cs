using IndWalksAPI.Data;
using IndWalksAPI.Models.DomainModels;
using IndWalksAPI.Models.DTO;
using IndWalksAPI.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IndWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepo regionRepo;

        public RegionController(IRegionRepo regionRepo)
        {
            this.regionRepo = regionRepo;
        }
        //private INDWalksDbContext dbContext;
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //here will get the info from DB
            var regions = await regionRepo.GetAllAsync();

            //here will map the model with DTO
            var regionDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //here will return the DTO
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regions = await regionRepo.GetByIdAsync(id);
            if (regions != null)
            {
                var regionDto = new RegionDTO
                {
                    Id = regions.Id,
                    Name = regions.Name,
                    Code = regions.Code,
                    RegionImageUrl = regions.RegionImageUrl
                };

                return Ok(regionDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var region = new Region
            {
                Name = createRegionDTO.Name,
                Code = createRegionDTO.Code,
                RegionImageUrl = createRegionDTO.RegionImageUrl
            };

            await regionRepo.CreateAsync(region);

            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateRegionDTO updateRegionDTO)
        {
            var regionDomain = new Region
            {
                Code = updateRegionDTO.Code,
                Name = updateRegionDTO.Name,
                RegionImageUrl = updateRegionDTO.RegionImageUrl
            };
            regionDomain = await regionRepo.UpdateAsync(id, regionDomain);
            if (regionDomain != null)
            {
                var regionDto = new RegionDTO
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                };

                return Ok(regionDto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var model = await regionRepo.DeleteAsync(id);
            if (model != null)
            {
                var regionDto = new RegionDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Code = model.Code,
                    RegionImageUrl = model.RegionImageUrl
                };

                return Ok(regionDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
