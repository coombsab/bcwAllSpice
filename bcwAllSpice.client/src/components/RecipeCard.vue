<template>
  <div class="recipe-card text-visible elevation-2 mb-3 fadeIn" :style="{ backgroundImage: `url(${recipe?.img})` }">
    <div class="layer selectable p-3" title="Recipe Details" data-bs-toggle="modal"
      :data-bs-target="'#recipeDetailsModal' + recipe.id" @click="getIngredientsByRecipeId()">
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

    <div class="heart selectable rounded fs-3" v-if="user.isAuthenticated">
      <i class="mdi mdi-heart-outline" type="button" @click="toggleFavorite()" v-if="!isFave()"
        title="Favorite Recipe"></i>
      <i class="mdi mdi-heart favorite" type="button" @click="toggleFavorite()" v-else title="Unfavorite Recipe"></i>
    </div>


  </div>
  <RecipeDetailsModal :routeName="routeName" :recipe="recipe" />
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
    recipe: { type: Recipe, required: true },
    routeName: { type: String }
  },
  setup(props) {

    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async toggleFavorite() {
        try {
          console.log(props.routeName)
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
          this.resetFormVisibility()
          await recipesService.getIngredientsByRecipeId(props.recipe.id)
        }
        catch (error) {
          Pop.error(error.message, "[getIngredients]")
        }
      },

      resetFormVisibility() {
        AppState.isEditTitleVisible = false
        AppState.isEditSubtitleVisible = false
        AppState.isEditImageVisible = false
        AppState.isEditInstructionsVisible = false
        AppState.isEditCategoryVisible = false
      }
    };
  },
  components: { RecipeDetailsModal }
}
</script>

<style scoped lang="scss">
.recipe-card {
  width: 90vw;
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

.fadeIn {
  animation: fadeIn ease 0.5s;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }

  100% {
    opacity: 1;
  }
}

@media (min-width: 768px) {
  .recipe-card {
    width: 30vw;
    height: 40vh;
    background-position: center;
    background-size: cover;
    border-radius: 0.5rem;
    position: relative;
    transition: 0.15s linear;
  }

  .recipe-card:hover {
    transform: scale(1.05);
  }
}
</style>