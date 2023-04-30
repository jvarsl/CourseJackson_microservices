using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Models;
using PlatformService.Models.Dtos;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatformsAsync()
        {
            var platforms = await _platformRepository.GetAllPlatformsAsync();
            var mappedPlatforms = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
            return Ok(mappedPlatforms);
        }

        [HttpGet("{id}", Name = "GetPlatformAsync")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformAsync(Guid id)
        {
            var platform = await _platformRepository.GetPlatformByIdAsync(id);
            if (platform is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            await _platformRepository.CreatePlatformAsync(platform);
            var result = await _platformRepository.SaveChangesAsync();
            if (!result)
            {
                return BadRequest();
            }
            var mappedPlatform = _mapper.Map<PlatformReadDto>(platform);
            return CreatedAtRoute(nameof(GetPlatformAsync), new { mappedPlatform.Id }, mappedPlatform);
        }
    }
}
