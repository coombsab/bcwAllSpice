<template>
  <section class="favorites">
    <div class="favorites-content d-flex flex-wrap gap-3 px-3 justify-content-evenly py-5 mt-5" v-if="favRecipes.length > 0">
      <RecipeCard v-for="r in favRecipes" :key="r.id" :recipe="r" />
    </div>
    <div v-else>
      <span class="fadeIn">Sorry, you have favorited no recipes!</span>
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
    async function getFavoriteRecipes() {
      try {
        await recipesService.getFavoriteRecipes()
      }
      catch(error) {
        Pop.error(error.message, "[getFavoriteRecipes]")
      }
    }

    onMounted(() => {
      getFavoriteRecipes()
    })

    return {
      favRecipes: computed(() => AppState.favRecipes)
    }
  }
}
</script>

<style scoped lang="scss">
  .favorites {
    display: flex;
    flex-direction: column;
    height: 80vh;
  }

  .favorites-content {
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