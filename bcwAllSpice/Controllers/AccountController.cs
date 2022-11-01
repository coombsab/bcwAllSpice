namespace bcwAllSpice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;
  private readonly RecipesService _recipesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService, RecipesService recipesService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
    _recipesService = recipesService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]
  [Authorize]
  public async Task<ActionResult<List<FavRecipe>>> GetFavorites()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<FavRecipe> favRecipes = _favoritesService.GetFavoritesByAccount(userInfo.Id);
      return Ok(favRecipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("recipes")]
  [Authorize]
  public async Task<ActionResult<List<Recipe>>> GetRecipesByAccount([FromQuery] string offset, [FromQuery] string limit)
  {
    try
    {
      Console.WriteLine("Before UserInfo");
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Console.WriteLine("After UserInfo");
      List<Recipe> recipes = _recipesService.GetRecipesByAccount(userInfo.Id, offset, limit);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
