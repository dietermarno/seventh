using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;
using SeventhCoreWebAPI.Repositories;

namespace SeventhCoreWebAPI.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class VideosController : Controller
    {
        private readonly IVideoRepository repository;

        public VideosController(IVideoRepository _context)
        {
            repository = _context;
        }

        [HttpPost("servers/{serverId}/videos")] //TODO: serverId será usado para determinar a pasta no filesystem
        public async Task<IActionResult> NewVideo(string serverId, [FromBody] Video video)
        {
            if (video == null)
            {
                return BadRequest("Video data is null");
            }
            video.ServerId = serverId;
            video.Id = new Guid().ToString();
            await repository.Insert(video);
            return CreatedAtAction(nameof(GetVideo), new { video.Id }, video);
        }

        [HttpDelete("servers/{serverId}/videos/{videoId}")] //TODO: serverId será usado para determinar a pasta no filesystem
        public async Task<ActionResult<Video>> DeleteCustomerAddress(string serverId, string videoId)
        {
            var video = await repository.GetById(videoId);
            if (video == null)
            {
                return NotFound($"Video Id {videoId} not found");
            }
            await repository.Delete(videoId);
            return Ok(video);
        }

        [HttpGet("servers/{serverId}/videos/{videoId}")] //TODO: serverId será usado para determinar a pasta no filesystem
        public async Task<ActionResult<Video>> GetVideo(string serverId, string videoId)
        {
            var video = await repository.GetById(videoId);
            if (video == null)
            {
                return NotFound($"No video found for id {videoId}");
            }
            return Ok(video);
        }

        [HttpGet("servers/{serverId}/videos/{videoId}/binary")] //TODO: serverId será usado para determinar a pasta no filesystem
        public async Task<ActionResult<Video>> DownloadVideo(string serverId, string videoId)
        {
            var video = await repository.GetById(videoId);
            if (video == null)
            {
                return NotFound($"No video found for id {videoId}");
            }
            return Ok(video);
        }

        [HttpGet("servers/{serverId}/videos")]
        public async Task<ActionResult<IEnumerable<Server>>> GetServerVideos(string serverId)
        {
            var videos = await repository.GetServerVideos(serverId);
            if (videos == null)
            {
                return NotFound($"No videos found for server id {serverId}");
            }
            return Ok(videos.ToList());
        }

        [HttpGet("recycler/process/{days}")]
        public async Task<ActionResult<IEnumerable<Video>>> RecycleProcess(int days)
        {
            return Ok("Não implementado.");
        }

        [HttpGet("recycler/status")]
        public async Task<ActionResult<IEnumerable<Video>>> RecycleStatus(int days)
        {
            return Ok("Não implementado.");
        }

        [HttpPut("servers/{serverId}/videos/{videoId}")]
        public async Task<IActionResult> UpdateVideo(string id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest($"Video Id {id} is invalid");
            }
            try
            {
                await repository.Update(id, video);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok($"Video Id {id} succefully updated");
        }
    }
}
