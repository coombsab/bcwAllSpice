import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Recipe.js').Recipe[]} */
  recipes: [],
  /** @type {import('./models/Recipe.js').Recipe[]} */
  favRecipes: [],
  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  ingredients: [],
  search: "",
  isEditTitleVisible: false,
  isEditSubtitleVisible: false,
  isEditImageVisible: false,
  isEditInstructionsVisible: false,
  isEditIngredientsVisible: false,
  isEditCategoryVisible: false,
  categories: ["Breakfast", "Lunch", "Dinner", "Dessert", "Mexican", "Italian", "Chinese", "Cheese", "Specialty coffee"]
})
