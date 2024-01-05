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
            server.Id = new Guid().ToString();
            await repository.Insert(server);
            return CreatedAtAction(nameof(GetServer), new { server.Id }, server);
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

        [HttpGet("servers/{serverId}")]
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
        public async Task<IEnumerable<Server>> ServerExists(string serverId)
        {
            return await repository.ServerExists(serverId);
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
                await repository.Update(id, server);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok($"Server Id {id} succefully updated");
        }
    }
}
