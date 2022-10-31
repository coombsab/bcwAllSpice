namespace bcwAllSpice.Services;

public class RecipesService {
  private readonly RecipesRepository _recipesRepository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  public Recipe CreateRecipe(Recipe recipeData, string userId) {
      recipeData.CreatorId = userInfo.Id;
    return _recipesRepository.CreateRecipe(recipeData);
  }

  public List<Recipe> GetAllRecipes() {
    return _recipesRepository.GetAllRecipes();
  }

  public Recipe GetRecipeById(int recipeId) {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
    if (recipe == null) {
      throw new Exception("Could not find recipe.  Invalid ID.");
    }
    return recipe;
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