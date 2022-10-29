<template>
  <div class="recipe-card text-visible elevation-2" :style="{ backgroundImage: `url(${recipe?.img})` }">
    <div class="layer selectable p-3" data-bs-toggle="modal" :data-bs-target="'#recipeDetailsModal' + recipe.id"
      @click="getIngredientsByRecipeId()">
      <div class="card-content">
        <div class="d-flex align-items-center fs-3">
          <span>{{ recipe.category }}</span>
        </div>
        <div class="d-flex flex-column">
          <span class="fs-3">{{ recipe.title }}</span>
          <span>{{ recipe.subtitle }}</span>
        </div>
      </div>
    </div>
    <i class="mdi mdi-heart-outline heart selectable rounded fs-3" type="button" @click="toggleFavorite()"
      v-if="!isFave()"></i>
    <i class="mdi mdi-heart heart selectable rounded favorite fs-3" type="button" @click="toggleFavorite()" v-else></i>

  </div>
  <RecipeDetailsModal :recipe="recipe" />
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";
import RecipeDetailsModal from "./RecipeDetailsModal.vue";

export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async toggleFavorite() {
        try {
          await recipesService.toggleFavorite(props.recipe.id)
        }
        catch (error) {
          Pop.error(error.message, "[function]")
        }
      },
      isFave() {
        if (props.recipe.favoriteeIds.find(id => id === this.account.id)) {
          return true
        } else {
          return false
        }
      },
      async getIngredientsByRecipeId() {
        try {
          await recipesService.getIngredientsByRecipeId(props.recipe.id)
        }
        catch (error) {
          Pop.error(error.message, "[getIngredients]")
        }
      }
    };
  },
  components: { RecipeDetailsModal }
}
</script>

<style scoped lang="scss">
.recipe-card {
  width: 30vw;
  height: 40vh;
  background-position: center;
  background-size: cover;
  border-radius: 0.5rem;
  position: relative;
}

.layer {
  height: 100%;
  width: 100%;
}

.heart {
  position: absolute;
  right: 2%;
  top: 2%;
}

.favorite {
  color: fuchsia;
}

.card-content {
  display: flex;
  flex-direction: column;
  height: 100%;
  justify-content: space-between;
}
</style>