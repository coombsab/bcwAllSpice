<template>
  <section class="my-recipes">
    <div class="my-recipes-content d-flex flex-wrap gap-3 px-3 justify-content-evenly py-5 mt-5" v-if="recipes.length > 0">
      <RecipeCard v-for="r in recipes" :key="r.id" :recipe="r" />
    </div>
    <div v-else>
      <span class="fadeIn">Sorry, you have no recipes!</span>
    </div>
  </section>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";

export default {
  setup() {
    async function getMyRecipes() {
      try {
        await recipesService.getMyRecipes();
      }
      catch (error) {
        Pop.error(error.message, "[getMyRecipes]");
      }
    }
    onMounted(() => {
      getMyRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes.filter(recipe => recipe.creatorId === AppState.account.id)),
    }
  }
}
</script>

<style scoped lang="scss">
  .my-recipes {
    display: flex;
    flex-direction: column;
    height: 80vh;
  }

  .my-recipes-content {
    flex-grow: 1;
    overflow-y: auto;
  }
  
.fadeIn {
  animation: fadeIn ease 12s;
}

@keyframes fadeIn {
  0% {
    opacity:0;
  }
  75% {
    opacity: 0;
  }
  100% {
    opacity:1;
  }
}
</style>