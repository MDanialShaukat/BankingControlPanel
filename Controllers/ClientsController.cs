using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = "RequireAdminRole")]
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var clients = await _clientService.GetClients(search, page, pageSize);
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] Client client)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _clientService.CreateClient(client);
        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
    {
        if (id != client.Id || !ModelState.IsValid)
            return BadRequest();

        var response = await _clientService.UpdateClient(client);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _clientService.GetClientById(id);
        if (client is null)
            return NotFound("The Client does not exists.");

        await _clientService.DeleteClient(id);
        return NoContent();
    }
}
