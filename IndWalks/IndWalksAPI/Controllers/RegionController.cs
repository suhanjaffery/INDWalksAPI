using IndWalksAPI.Data;
using IndWalksAPI.Models.DomainModels;
using IndWalksAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private INDWalksDbContext dbContext;
        public RegionController(INDWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //here will get the info from DB
            var regions = dbContext.regions.ToList();

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
        public IActionResult GetById(Guid id)
        {
            var regions = dbContext.regions.FirstOrDefault(c => c.Id == id);
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
        public ActionResult Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var region = new Region
            {
                Name = createRegionDTO.Name,
                Code = createRegionDTO.Code,
                RegionImageUrl = createRegionDTO.RegionImageUrl
            };

            dbContext.regions.Add(region);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute]Guid id, [FromBody]UpdateRegionDTO updateRegionDTO)
        {
            var regionModel = dbContext.regions.FirstOrDefault(x => x.Id == id);
            if (regionModel != null)
            {
                regionModel.Name = updateRegionDTO.Name;
                regionModel.Code = updateRegionDTO.Code;
                regionModel.RegionImageUrl = updateRegionDTO.RegionImageUrl;

                //dbContext.regions.Update(regionModel);
                dbContext.SaveChanges();

                var regionDto = new RegionDTO
                {
                    Id = regionModel.Id,
                    Name = regionModel.Name,
                    Code = regionModel.Code,
                    RegionImageUrl = regionModel.RegionImageUrl
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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var model = dbContext.regions.Find(id);
            if (model != null)
            {
                dbContext.regions.Remove(model);
                dbContext.SaveChanges();

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
