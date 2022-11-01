namespace bcwAllSpice.Repositories;

public class RecipesRepository : RepositoryBase
{
  public RecipesRepository(IDbConnection db) : base(db)
  {
  }

  public Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
      INSERT INTO recipes(title, instructions, img, category, creatorId, subtitle)
      VALUES(@Title, @Instructions, @Img, @Category, @CreatorId, @Subtitle);
      SELECT LAST_INSERT_ID();
    ";
    recipeData.Id = _db.ExecuteScalar<int>(sql, recipeData);
    return GetRecipeById(recipeData.Id);
  }
  public List<Recipe> GetAllRecipes(int offset, int limit)
  {
    string sql = @"
    SELECT rec.*, acc.* FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId
    ORDER by rec.updatedAt DESC
    LIMIT @limit
    OFFSET @offset;
    ";
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { offset, limit }).ToList();

    sql = @"
      SELECT acc.* FROM favorites fav
      JOIN accounts acc ON acc.id = fav.accountId
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe =>
    {
      int recipeId = recipe.Id;
      recipe.Favoritees = _db.Query<Profile>(sql, new { recipeId }).ToList();
    });

    sql = @"
      SELECT accountId FROM favorites
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe =>
    {
      int recipeId = recipe.Id;
      recipe.FavoriteeIds = _db.Query<string>(sql, new { recipeId }).ToList();
    });

    return recipes;
  }

  public List<Recipe> GetRecipesByAccount(string accountId, int offset, int limit)
  {
    // string sql = @"
    //   SELECT * FROM recipes
    //   WHERE creatorId = @accountId
    // ";

    // List<Recipe> recipes = _db.Query<Recipe>(sql, new { accountId }).ToList();
    
    string sql = @"
    SELECT rec.*, acc.* FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE rec.creatorId = @accountId
    ORDER by rec.updatedAt DESC
    LIMIT @limit
    OFFSET @offset;
    ";
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { accountId, offset, limit }).ToList();

    sql = @"
      SELECT acc.* FROM favorites fav
      JOIN accounts acc ON acc.id = fav.accountId
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe =>
    {
      int recipeId = recipe.Id;
      recipe.Favoritees = _db.Query<Profile>(sql, new { recipeId }).ToList();
    });

    sql = @"
      SELECT accountId FROM favorites
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe =>
    {
      int recipeId = recipe.Id;
      recipe.FavoriteeIds = _db.Query<string>(sql, new { recipeId }).ToList();
    });

    return recipes;
  }

  public Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
      SELECT rec.*, acc.* FROM recipes rec
      JOIN accounts acc ON acc.id = rec.creatorId
      WHERE rec.id = @recipeId;
    ";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();

    sql = @"
      SELECT acc.* FROM favorites fav
      JOIN accounts acc ON acc.id = fav.accountId
      WHERE recipeId = @Id
    ";

    recipe.Favoritees = _db.Query<Profile>(sql, recipe).ToList();

    sql = @"
      SELECT accountId FROM favorites
      WHERE recipeId = @Id
    ";

    recipe.FavoriteeIds = _db.Query<string>(sql, recipe).ToList();

    return recipe;
  }

  public FavRecipe GetFavRecipeById(int recipeId)
  {
    string sql = @"
      SELECT rec.*, acc.* FROM recipes rec
      JOIN accounts acc ON acc.id = rec.creatorId
      WHERE rec.id = @recipeId;
    ";

    FavRecipe recipe = _db.Query<FavRecipe, Profile, FavRecipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();


    sql = @"
      SELECT acc.* FROM favorites fav
      JOIN accounts acc ON acc.id = fav.accountId
      WHERE recipeId = @Id
    ";

    recipe.Favoritees = _db.Query<Profile>(sql, recipe).ToList();

    sql = @"
      SELECT accountId FROM favorites
      WHERE recipeId = @Id
    ";

    recipe.FavoriteeIds = _db.Query<string>(sql, recipe).ToList();

    return recipe;
  }

  public Recipe EditRecipe(Recipe recipe)
  {
    string sql = @"
      UPDATE recipes
      SET title = @Title, instructions = @Instructions, img = @Img, category = @Category, subtitle = @Subtitle
      WHERE id = @Id;
    ";
    var rows = _db.Execute(sql, recipe);
    if (rows < 1)
    {
      throw new Exception("Changes were not saved.");
    }
    if (rows > 1)
    {
      throw new Exception("Something went wrong with the edit.  Contact your DBA.");
    }
    return recipe;
  }

  public void DeleteRecipe(int recipeId)
  {
    string sql = @"
      DELETE FROM recipes
      WHERE id = @recipeId;
    ";

    var rows = _db.Execute(sql, new { recipeId });
    if (rows < 1)
    {
      throw new Exception("Recipe probably was not deleted.");
    }
    if (rows > 1)
    {
      throw new Exception("Something went wrong with the delete.  Contact your DBA.");
    }
  }
}