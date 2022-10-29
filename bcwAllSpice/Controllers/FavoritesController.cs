namespace bcwAllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase {
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;

  public FavoritesController(Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
  }

  [HttpPost]
  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData) {
    try {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      Favorite favorite = _favoritesService.CreateFavorite(favoriteData);
      return Ok(favorite);
    }
    catch(Exception e) {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{favoriteId}")]
  public async Task<ActionResult<Favorite>> DeleteFavorite(int favoriteId) {
    try {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Favorite favorite = _favoritesService.DeleteFavorite(favoriteId, userInfo.Id);
      return Ok(favorite);
    }
    catch(Exception e) {
      return BadRequest(e.Message);
    }
  }
}