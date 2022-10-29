namespace bcwAllSpice.Repositories;

public class IngredientsRepository : RepositoryBase
{
  public IngredientsRepository(IDbConnection db) : base(db)
  {
  }

  public Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
      INSERT INTO ingredients(name, quantity, recipeId)
      VALUES(@Name, @Quantity, @RecipeId);
      SELECT LAST_INSERT_ID();
    ";
    ingredientData.Id = _db.ExecuteScalar<int>(sql, ingredientData);
    return ingredientData;
  }

  public List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
      SELECT * FROM ingredients
      WHERE recipeId = @recipeId;
    ";

    return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
  }

  public Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
      SELECT * FROM ingredients
      WHERE id = @ingredientId;
    ";

    return _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
  }

  public void DeleteIngredient(int ingredientId)
  {
    string sql = @"
      DELETE FROM ingredients
      WHERE id = @ingredientId;
    ";

    _db.Execute(sql, new { ingredientId });
  }
}