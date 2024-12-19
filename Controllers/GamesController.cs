using GameCollectionApi.Models;
using GameCollectionApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly GameContext _context;

    public GamesController(GameContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all games in the collection.
    /// </summary>
    /// <returns>A list of games.</returns>

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all games", Description = "Gets a list of all games stored in the collection.")]
    [SwaggerResponse(200, "List of games retrieved successfully.")]
    [SwaggerResponse(500, "Internal server error.")]
    public async Task<ActionResult<IEnumerable<Game>>> GetGames() => await _context.Games.ToListAsync();

    /// <summary>
    /// Get a specific game by ID.
    /// </summary>
    /// <param name="id">ID of the game to retrieve.</param>
    /// <returns>The requested game.</returns>

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieve a game by ID", Description = "Gets a single game from the collection using its ID.")]
    [SwaggerResponse(200, "Game retrieved successfully.")]
    [SwaggerResponse(404, "Game not found.")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game = await _context.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return game;
    }

    /// <summary>
    /// Add a new game to the collection.
    /// </summary>
    /// <param name="game">The game to add.</param>
    /// <returns>The added game.</returns>

    [HttpPost]
    [SwaggerOperation(Summary = "Add a new game", Description = "Creates a new game entry in the database.")]
    [SwaggerResponse(201, "Game added successfully.")]
    [SwaggerResponse(400, "Invalid input.")]
    public async Task<ActionResult<Game>> PostGame([FromForm] Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGame), new { id = game.GameId }, game);
    }

    /// <summary>
    /// Delete a game from the collection.
    /// </summary>
    /// <param name="id">ID of the game to delete.</param>
    /// <returns>No content if successful.</returns>

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a game", Description = "Removes a game from the collection by ID.")]
    [SwaggerResponse(204, "Game deleted successfully.")]
    [SwaggerResponse(404, "Game not found.")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return NotFound();
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
