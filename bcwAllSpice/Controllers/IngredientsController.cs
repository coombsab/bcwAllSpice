namespace bcwAllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase {
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;

  public IngredientsController(Auth0Provider auth0Provider, IngredientsService ingredientsService)
  {
    _auth0Provider = auth0Provider;
    _ingredientsService = ingredientsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData) {
    try {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);
      return Ok(ingredient);
    }
    catch(Exception e) {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  [Authorize]
  public async Task<ActionResult<Ingredient>> DeleteIngredient(int ingredientId) {
    try {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
      return Ok(ingredient);
    }
    catch(Exception e) {
      return BadRequest(e.Message);
    }
  }
}