namespace bcwAllSpice.Repositories;

public class RecipesRepository : RepositoryBase
{
  public RecipesRepository(IDbConnection db) : base(db)
  {
  }

  public Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
      INSERT INTO recipes(title, instructions, img, category, creatorId)
      VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);
      SELECT LAST_INSERT_ID();
    ";
    recipeData.Id = _db.ExecuteScalar<int>(sql, recipeData);
    return recipeData;
  }
  public List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT rec.*, acc.* FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId;
    ";
    // TODO AsList?  Why will ToList not work?
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) => {
      recipe.Creator = profile;
      return recipe;
    }).ToList();

    sql = @"
      SELECT acc.* FROM favorites fav
      JOIN accounts acc ON acc.id = fav.accountId
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe => {
      int recipeId = recipe.Id;
      recipe.Favoritees = _db.Query<Profile>(sql, new { recipeId }).ToList();
    });

    sql = @"
      SELECT accountId FROM favorites
      WHERE recipeId = @recipeId
    ";

    recipes.ForEach(recipe => {
      int recipeId = recipe.Id;
      recipe.FavoriteeIds = _db.Query<string>(sql, new { recipeId }).ToList();
    });

    return recipes;
  }

  public Recipe GetRecipeById(int recipeId) {
    string sql = @"
      SELECT rec.*, acc.* FROM recipes rec
      JOIN accounts acc ON acc.id = rec.creatorId
      WHERE rec.id = @recipeId;
    ";

    return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) => {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
  }

  public FavRecipe GetFavRecipeById(int recipeId) {
    string sql = @"
      SELECT rec.*, acc.* FROM recipes rec
      JOIN accounts acc ON acc.id = rec.creatorId
      WHERE rec.id = @recipeId;
    ";

    FavRecipe recipe = _db.Query<FavRecipe, Profile, FavRecipe>(sql, (recipe, profile) => {
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

  public Recipe EditRecipe(Recipe recipe) {
    string sql = @"
      UPDATE recipes
      SET title = @Title, instructions = @Instructions, img = @Img, category = @Category
      WHERE id = @Id;
    ";
    _db.Execute(sql, recipe);
    return recipe;
  }

  public void DeleteRecipe(int recipeId)
  {
    string sql = @"
      DELETE FROM recipes
      WHERE id = @recipeId;
    ";

    _db.Execute(sql, new { recipeId });
  }
}