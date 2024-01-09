using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;
using SeventhCoreWebAPI.Repositories;
using System.Text;

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
            if (!repository.IsServerAvailable(serverId).Result)
            {
                return new ObjectResult($"Servidor Id {serverId} not found") { StatusCode = StatusCodes.Status404NotFound};
            }
            if (video == null)
            {
                return BadRequest("Video data is null");
            }
            video.ServerId = serverId;
            Guid guid = Guid.NewGuid();
            video.Id = guid.ToString();
            video.SizeInBytes = UploadVideo(serverId, video.Id, video.Base64Binary);
            await repository.Insert(video);
            return new ObjectResult(video) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("servers/{serverId}/videos/{videoId}")] //TODO: serverId será usado para determinar a pasta no filesystem
        public async Task<ActionResult<Video>> DeleteVideo(string serverId, string videoId)
        {
            if (!repository.IsServerAvailable(serverId).Result)
            {
                return new ObjectResult($"Servidor Id {serverId} not found") { StatusCode = StatusCodes.Status404NotFound };
            }
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
            if (!repository.IsServerAvailable(serverId).Result)
            {
                return new ObjectResult($"Servidor Id {serverId} not found") { StatusCode = StatusCodes.Status404NotFound };
            }
            var video = await repository.GetById(videoId);
            if (video == null)
            {
                return NotFound($"No video found for id {videoId}");
            }
            return Ok(video);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public long UploadVideo(string serverId, string videoId, string base64Content)
        {
            string dataPath = @$"/data/{serverId}";
            Directory.CreateDirectory(dataPath);
            byte[] fileData = Convert.FromBase64String(base64Content);
            System.IO.File.WriteAllBytes(@$"{dataPath}/{videoId}.bin", fileData);
            return fileData.LongLength;
        }

        [HttpGet("servers/{serverId}/videos/{videoId}/binary")]
        public IActionResult DownLoad(string serverId, string videoId)
        {
            string path = @$"/data/{serverId}/{videoId}.bin";
            if (System.IO.File.Exists(path))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", $"{videoId}.bin");
            }
            else
            {
                return NotFound("File not exist");
            }
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
        public ActionResult RecycleProcess(int days)
        {
            return Ok("Não implementado.");
        }

        [HttpGet("recycler/status")]
        public ActionResult RecycleStatus(int days)
        {
            return Ok("Não implementado.");
        }

        [HttpPut("servers/{serverId}/videos/{videoId}")]
        public async Task<IActionResult> UpdateVideo(string serverId, string videoId, [FromBody] Video video)
        {
            if (!repository.IsServerAvailable(serverId).Result)
            {
                return new ObjectResult($"Servidor Id {serverId} not found") { StatusCode = StatusCodes.Status404NotFound };
            }
            if (videoId != video.Id)
            {
                return BadRequest($"Video Id {videoId} is invalid");
            }
            try
            {
                await repository.Update(videoId, video);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok($"Video Id {videoId} succefully updated");
        }
    }
}
