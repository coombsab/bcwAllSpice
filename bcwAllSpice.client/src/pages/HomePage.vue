<template>
  <section class="home">
    <div class="home-content d-flex flex-wrap gap-5 px-3 justify-content-evenly py-5 mt-5" v-if="recipes.length > 0">
      <RecipeCard v-for="r in recipes" :key="r.id" :recipe="r" />
    </div>
    <div v-else>
      <span class="fadeIn">No recipes currently available, please add some!</span>
    </div>
  </section>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import RecipeCard from "../components/RecipeCard.vue";
import { recipesService } from "../services/RecipesService.js"
import Pop from "../utils/Pop";

export default {
    setup() {
        async function getAllRecipes() {
            try {
                await recipesService.getAllRecipes();
            }
            catch (error) {
                Pop.error(error.message, "[getAllRecipes]");
            }
        }
        onMounted(() => {
            getAllRecipes();
        });
        return {
            recipes: computed(() => AppState.recipes)
        };
    },
    components: { RecipeCard }
}
</script>

<style scoped lang="scss">
  .home {
    display: flex;
    flex-direction: column;
    height: 80vh;
  }

  .home-content {
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
