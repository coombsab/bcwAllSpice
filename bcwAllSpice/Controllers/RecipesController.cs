namespace bcwAllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;
  private readonly FavoritesService _favoritesService;

  public RecipesController(Auth0Provider auth0Provider, RecipesService recipesService, IngredientsService ingredientsService, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
    _favoritesService = favoritesService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.CreateRecipe(recipeData, userInfo.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes([FromQuery] string offset, [FromQuery] string limit)
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetAllRecipes(offset, limit);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> EditRecipe([FromBody] Recipe recipeData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _recipesService.EditRecipe(recipeData, userInfo.Id, recipeId);
      return Ok(recipeData);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.DeleteRecipe(recipeId, userInfo.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}/favorite")]
  [Authorize]
  public async Task<ActionResult<FavRecipe>> ToggleLiked(int recipeId) {
    try {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      FavRecipe recipe = _favoritesService.ToggleLiked(recipeId, userInfo.Id);
      return Ok(recipe);
    }
    catch(Exception e) {
      return BadRequest(e.Message);
    }
  }
}