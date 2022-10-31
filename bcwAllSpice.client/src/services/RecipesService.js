import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { Recipe } from "../models/Recipe"
import { api } from "./AxiosService"

class RecipesService {
  async getAllRecipes() {
    AppState.recipes = []
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(data => new Recipe(data))
  }

  async getFavoriteRecipes() {
    AppState.recipes = []
    const res = await api.get("account/favorites")
    AppState.recipes = res.data.map(data => new Recipe(data))

  }

  async getMyRecipes() {
    AppState.recipes = []
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(data => new Recipe(data))

  }

  async toggleFavorite(recipeId) {
    let recipe = AppState.recipes.find(r => r.id == recipeId)
    const recipeIndex = AppState.recipes.indexOf(recipe)
    if (!recipe) {
      throw new Error("Cannot toggle favorite due to invalid recipe ID.")
    }
    const res = await api.put(`api/recipes/${recipeId}/favorite`)

    recipe = new Recipe(res.data)
    AppState.recipes.splice(recipeIndex, 1, recipe)

    let favRecipe = AppState.favRecipes.find(r => r.id === recipeId)
    if (!favRecipe) {
      AppState.favRecipes.push(recipe)
    } else {
      AppState.favRecipes = AppState.favRecipes.filter(r => r.id !== recipeId)
    }
  }

  async createRecipe(recipeData) {
    const res = await api.post("api/recipes", recipeData)
    AppState.recipes.push(new Recipe(res.data))
  }

  async getIngredientsByRecipeId(recipeId) {
    AppState.ingredients = []
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.ingredients = res.data.map(data => new Ingredient(data))
  }

  async editRecipe(recipeData) {
    console.log("Editing recipe", recipeData)
    const recipe = AppState.recipes.find(r => r.id === recipeData.id)
    const favRecipe = AppState.favRecipes.find(r => r.id === recipeData.id)
    if (!recipe && !favRecipe) {
      throw new Error("Cannot find recipe to edit.  Invalid ID.")
    }
    const res = await api.put(`api/recipes/${recipeData.id}`, recipeData)
    const recipeIndex = AppState.recipes.indexOf(recipe)
    AppState.recipes.splice(recipeIndex, 1, new Recipe(res.data))
    const favRecipeIndex = AppState.recipes.indexOf(favRecipe)
    AppState.favRecipes.splice(favRecipeIndex, 1, new Recipe(res.data))
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete(`api/recipes/${recipeId}`)
    AppState.recipes = AppState.recipes.filter(recipe => recipe.id !== recipeId)
    AppState.favRecipes = AppState.favRecipes.filter(recipe => recipe.id !== recipeId)
  }
}

export const recipesService = new RecipesService()