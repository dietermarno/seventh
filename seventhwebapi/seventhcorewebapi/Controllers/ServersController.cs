using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;
using SeventhCoreWebAPI.Repositories;

namespace SeventhCoreWebAPI.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class ServersController : Controller
    {
        private readonly IServerRepository repository;
        public ServersController(IServerRepository _context)
        {
            repository = _context;
        }

        [HttpPost("server")]
        public async Task<IActionResult> NewServer([FromBody] Server server)
        {
            if (server == null)
            {
                return BadRequest("Server information is null");
            }
            Guid guid = Guid.NewGuid();
            server.Id = guid.ToString();
            await repository.Insert(server);
            return new ObjectResult(server) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("servers/{serverId}")]
        public async Task<ActionResult<Server>> DeleteCustomer(string serverId)
        {
            var server = await repository.GetById(serverId);
            if (server == null)
            {
                return NotFound($"Server Id {serverId} not found");
            }
            await repository.Delete(serverId);
            return Ok(server);
        }

        [HttpGet("servers/{serverId}", Name="GetServer")]
        public async Task<ActionResult<Server>> GetServer(string serverId)
        {
            var server = await repository.GetById(serverId);
            if (server == null)
            {
                return NotFound($"Server Id {serverId} not found");
            }
            return Ok(server);
        }

        [HttpGet("servers/available/{serverId}")]
        public async Task<IActionResult> ServerExists(string serverId)
        {
            var server = await repository.ServerExists(serverId);
            if (server.Count() == 0)
                return new ObjectResult(server) { StatusCode = StatusCodes.Status404NotFound };
            else
                return new ObjectResult(server) { StatusCode = StatusCodes.Status302Found };
        }

        [HttpGet("servers")]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            var servers = await repository.GetAll();
            if (servers == null)
            {
                return BadRequest();
            }
            return Ok(servers.ToList());
        }

        [HttpPut("servers/{id}")]
        public async Task<IActionResult> UpdateServer(string id, Server server)
        {
            if (id != server.Id)
            {
                return BadRequest($"Server Id {id} is invalid");
            }
            try
            {
                if (repository.ServerExists(id).Result.Any())
                    await repository.Update(id, server);
                else
                    return new ObjectResult($"Server Id {id} not found") { StatusCode = StatusCodes.Status404NotFound };
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok($"Server Id {id} succefully updated");
        }
    }
}
