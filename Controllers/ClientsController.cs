using AutoMapper;
using Azure;
using BankingControlPanel.Dtos;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Contains endpoints for the management of Clients
/// </summary>
[Authorize(Roles = "ADMIN")]
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get client details and also perform search operation
    /// </summary>
    /// <param name="search"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    [HttpGet]
    public async Task<IActionResult> GetClients([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var clients = await _clientService.GetClients(search, page, pageSize);
        var finalResponse = _mapper.Map<IEnumerable<ClientDto>>(clients);
        return Ok(finalResponse);
    }

    /// <summary>
    /// Create client
    /// </summary>
    /// <param name="client"></param>
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] Client client)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _clientService.CreateClient(client);
        var finalResponse = _mapper.Map<ClientDto>(response);
        return Created(string.Empty, finalResponse);
    }

    /// <summary>
    /// Update client
    /// </summary>
    /// <param name="client"></param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
    {
        if (id != client.Id || !ModelState.IsValid)
            return BadRequest();

        var response = await _clientService.UpdateClient(client);
        var finalResponse = _mapper.Map<ClientDto>(response);
        return Ok(finalResponse);
    }

    /// <summary>
    /// Delete client
    /// </summary>
    /// <param name="client"></param>
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
