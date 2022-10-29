import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { api } from "./AxiosService"

class RecipesService {
  async getAllRecipes() {
    AppState.recipes = []
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(data => new Recipe(data))
  }

  async getFavoriteRecipes() {
    AppState.favRecipes = []
    const res = await api.get("account/favorites")
    AppState.favRecipes = res.data.map(data => new Recipe(data))
    
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
}

export const recipesService = new RecipesService()