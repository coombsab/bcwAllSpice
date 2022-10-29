namespace bcwAllSpice.Services;

public class IngredientsService {
  private readonly IngredientsRepository _ingredientsRepository;
  private readonly RecipesService _recipeService;

  public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
  {
    _ingredientsRepository = ingredientsRepository;
    _recipeService = recipesService;
  }

  public Ingredient CreateIngredient(Ingredient ingredientData, string userId)
  {
    Recipe recipe = _recipeService.GetRecipeById(ingredientData.RecipeId);
    if (recipe.CreatorId != userId) {
      throw new Exception("You cannot add ingredients to this recipe as you did not create it.");
    }
    return _ingredientsRepository.CreateIngredient(ingredientData);
  }

  public List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    Recipe recipe = _recipeService.GetRecipeById(recipeId);
    return _ingredientsRepository.GetIngredientsByRecipeId(recipe.Id);
  }

  public Ingredient GetIngredientById(int ingredientId) {
    return _ingredientsRepository.GetIngredientById(ingredientId);
  }

  public Ingredient DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipeService.GetRecipeById(ingredient.RecipeId);
    if (recipe.CreatorId != userId) {
      throw new Exception("You did not create the recipe associated with this ingredient so may not delete it.");
    }
    _ingredientsRepository.DeleteIngredient(ingredientId);
    return ingredient;
  }
}