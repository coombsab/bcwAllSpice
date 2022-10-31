import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { api } from "./AxiosService"

class IngredientsService {
  async addIngredient(ingredientData, recipeId) {
    // console.log("adding ingredient", ingredientData, "to", recipeId)
    ingredientData.recipeId = recipeId
    console.log(ingredientData)
    const res = await api.post("api/ingredients", ingredientData)
    AppState.ingredients.push(new Ingredient(res.data))
  }

  async deleteIngredient(ingredientId) {
    const res = await api.delete(`api/ingredients/${ingredientId}`)
    AppState.ingredients = AppState.ingredients.filter(ingredient => ingredient.id !== ingredientId)
  }

  // async editIngredient(ingredientData, ingredientId) {
  //   const res = await api.put(`api/ingredients/${ingredientId}`, ingredientData)
  //   const ingredient = AppState.ingredients.find(i => i.id === ingredientId)
  //   const ingredientIndex = AppState.ingredients.indexOf(ingredient)
  //   AppState.ingredients.splice(ingredientIndex, 1, new Ingredient(res.data))
  // }
}

export const ingredientsService = new IngredientsService()