namespace bcwAllSpice.Services;

public class RecipesService {
  private readonly RecipesRepository _recipesRepository;

  private const int OFFSET_DEFAULT = 0;
  private const int LIMIT_DEFAULT = 100;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  public Recipe CreateRecipe(Recipe recipeData, string userId) {
      recipeData.CreatorId = userId;
    return _recipesRepository.CreateRecipe(recipeData);
  }

  public List<Recipe> GetAllRecipes(string offsetStr, string limitStr) {
    int offset = OFFSET_DEFAULT, limit = LIMIT_DEFAULT;
    if (offsetStr != null) {
      offset = int.Parse(offsetStr);
    } 

    if (limitStr != null) {
      limit = int.Parse(limitStr);
    }
    return _recipesRepository.GetAllRecipes(offset, limit);
  }

  public Recipe GetRecipeById(int recipeId) {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
    if (recipe == null) {
      throw new Exception("Could not find recipe.  Invalid ID.");
    }
    return recipe;
  }

  public List<Recipe> GetRecipesByAccount(string accountId, string offsetStr, string limitStr)
  {
    int offset = OFFSET_DEFAULT, limit = LIMIT_DEFAULT;
    if (offsetStr != null) {
      offset = int.Parse(offsetStr);
    } 

    if (limitStr != null) {
      limit = int.Parse(limitStr);
    }

    return _recipesRepository.GetRecipesByAccount(accountId, offset, limit);
  }

  public FavRecipe GetFavRecipeById(int recipeId) {
    FavRecipe recipe = _recipesRepository.GetFavRecipeById(recipeId);
    if (recipe == null) {
      throw new Exception("Could not find recipe.  Invalid ID.");
    }
    return recipe;
  }
  
  public Recipe EditRecipe(Recipe recipeData, string userId, int recipeId) {
    recipeData.Id = recipeId;
    Recipe recipe = GetRecipeById(recipeData.Id);
    if (recipe.CreatorId != userId) {
      throw new Exception("That is not your recipe.  You may not edit it.");
    }
    recipe.Title = recipeData.Title ?? recipe.Title;
    recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
    recipe.Img = recipeData.Img ?? recipe.Img;
    recipe.Category = recipeData.Category ?? recipe.Category;
    recipe.Subtitle = recipeData.Subtitle ?? recipe.Subtitle;
    return _recipesRepository.EditRecipe(recipe);
  }

  public Recipe DeleteRecipe(int recipeId, string userId) {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId) {
      throw new Exception("That is not your recipe.  You may not delete it.");
    }
    _recipesRepository.DeleteRecipe(recipeId);
    return recipe;
  }
}