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
    AppState.recipes = []
    const res = await api.get("account/favorites")
    AppState.recipes = res.data.map(data => new Recipe(data))
    
  }

  async getMyRecipes() {
    AppState.recipes = []
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(data => new Recipe(data))

  }
}

export const recipesService = new RecipesService()