namespace bcwAllSpice.Services;

public class FavoritesService {
  private readonly FavoritesRepository _favoritesRepository;
  private readonly RecipesService _recipesService;

  public FavoritesService(FavoritesRepository favoritesRepository, RecipesService recipesService)
  {
    _favoritesRepository = favoritesRepository;
    _recipesService = recipesService;
  }

  public Favorite GetFavoriteById(int favoriteId) {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
    if (favorite == null) {
      throw new Exception("Cannot get favorite.  Invalid ID.");
    }
    return favorite;
  }

  public Favorite CreateFavorite(Favorite favoriteData)
  {
    if (IsRecipeAlreadyFavorited(favoriteData)) {
      throw new Exception("This recipe is already favorited.");
    }
    return _favoritesRepository.CreateFavorite(favoriteData);
  }

  public bool IsRecipeAlreadyFavorited(Favorite favoriteData) {
    Favorite favorite = _favoritesRepository.GetFavorite(favoriteData);
    if (favorite == null) {
      return false;
    }
    return true;
  }

  public List<FavRecipe> GetFavoritesByAccount(string accountId)
  {
    return _favoritesRepository.GetFavoritesByAccount(accountId);
  }

  public Favorite DeleteFavorite(int favoriteId, string accountId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != accountId) {
      throw new Exception("This favorite is not yours to delete.");
    }

    _favoritesRepository.DeleteFavorite(favorite.Id);
    return favorite;
  }

  public Recipe ToggleLiked(int recipeId, string userId)
  {
    Recipe recipe = _recipesService.GetRecipeById(recipeId);
    Favorite favorite = _favoritesRepository.GetFavorite(recipeId, userId);

    if (favorite != null) {
      _favoritesRepository.DeleteFavorite(favorite.Id);
    } else {
      favorite = _favoritesRepository.CreateFavorite(recipeId, userId);
    }
    
    return recipe;
  }
}