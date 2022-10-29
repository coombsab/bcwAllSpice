namespace bcwAllSpice.Repositories;

public class FavoritesRepository : RepositoryBase
{
  public FavoritesRepository(IDbConnection db) : base(db)
  {
  }

  public Favorite GetFavoriteById(int favoriteId)
  {
    string sql =@"
      SELECT * FROM favorites
      WHERE id = @favoriteId;
    ";

    return _db.QueryFirstOrDefault<Favorite>(sql, new { favoriteId });
  }

  public Favorite GetFavorite(Favorite favoriteData)
  {
    string sql = @"
      SELECT * FROM favorites
      WHERE accountId = @AccountId AND recipeId = @RecipeId;
    ";

    return _db.QueryFirstOrDefault<Favorite>(sql, favoriteData);
  }

  public Favorite GetFavorite(int recipeId, string accountId) {
    string sql = @"
      SELECT * FROM favorites
      WHERE accountId = @accountId AND recipeId = @recipeId;
    ";

    return _db.QueryFirstOrDefault<Favorite>(sql, new { accountId, recipeId });
  }

  public Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql =@"
      INSERT INTO favorites(accountId, recipeId)
      VALUES(@AccountId, @RecipeId);
      SELECT LAST_INSERT_ID();
    ";

    favoriteData.Id = _db.ExecuteScalar<int>(sql, favoriteData);

    return favoriteData;
  }

  public Favorite CreateFavorite(int recipeId, string accountId) {
    string sql =@"
      INSERT INTO favorites(accountId, recipeId)
      VALUES(@AccountId, @RecipeId);
      SELECT LAST_INSERT_ID();
    ";

    int favoriteId = _db.ExecuteScalar<int>(sql, new { recipeId, accountId });
    
    return GetFavoriteById(favoriteId);
  }

  public List<FavRecipe> GetFavoritesByAccount(string accountId)
  {
    string sql = @"
      SELECT 
        rec.*, 
        fav.id AS FavoriteId,
        acc.*
      FROM favorites fav
      JOIN recipes rec ON rec.id = fav.recipeId
      JOIN accounts acc ON acc.id = rec.creatorId
      WHERE fav.accountId = @accountId
      GROUP BY fav.id;
    ";

    return _db.Query<FavRecipe, Profile, FavRecipe>(sql, (recipe, profile) => {
      recipe.Creator = profile;
      return recipe;
    }, new { accountId }).ToList();
  }

  public void DeleteFavorite(int favoriteId) {
    string sql = @"
      DELETE FROM favorites
      WHERE id = @favoriteId;
    ";

    _db.Execute(sql, new { favoriteId });
  }

  public void ToggleLiked(int recipeId, string userId)
  {
    throw new NotImplementedException();
  }
}