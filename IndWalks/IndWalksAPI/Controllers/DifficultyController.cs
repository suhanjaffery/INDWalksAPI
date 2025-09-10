using IndWalksAPI.Data;
using IndWalksAPI.Models.DomainModels;
using IndWalksAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly INDWalksDbContext dbContext;

        public DifficultyController(INDWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = dbContext.difficulties.ToList();
            if (data == null)
            {
                return NotFound();
            }
            var diffDto = new List<DifficultiesDTO>();

            foreach (var difficulty in data)
            {
                diffDto.Add(new DifficultiesDTO() {
                    Id = difficulty.Id,
                    Name = difficulty.Name,
                });
            }

            return Ok(diffDto);
        }
        [HttpGet]
        [Route("{id=Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var model = dbContext.difficulties.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            var diffDto = new DifficultiesDTO
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(diffDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody]DifficultiesDTO difficultiesDTO)
        {
            var diffModel = new Difficulty
            {
                Id = difficultiesDTO.Id,
                Name = difficultiesDTO.Name,
            };

            dbContext.difficulties.Add(diffModel);
            dbContext.SaveChanges();

            difficultiesDTO.Id = diffModel.Id;
            difficultiesDTO.Name = diffModel.Name;

            return Ok(difficultiesDTO);
        }
        [HttpPut]
        [Route("{id=Guid}")]
        public IActionResult Update([FromBody]DifficultiesDTO difficultiesDTO, [FromRoute] Guid id)
        {
            var diffModel = dbContext.difficulties.FirstOrDefault(x => x.Id == id);
            if (diffModel == null)
            {
                return NotFound();
            }

            dbContext.SaveChanges();

            difficultiesDTO.Id = diffModel.Id;
            difficultiesDTO.Name = diffModel.Name;

            return Ok(difficultiesDTO);
        }
        [HttpDelete]
        [Route("{id=Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var model = dbContext.difficulties.FirstOrDefault(x => x.Id == id);
            if (model == null) { return NotFound(); }

            dbContext.difficulties.Remove(model);

            var diffDto = new DifficultiesDTO
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(diffDto);
        }
    }
}
