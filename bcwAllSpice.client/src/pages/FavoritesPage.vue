<template>
  <section class="favorites">
    <div class="favorites-content d-flex flex-wrap gap-3 px-3 justify-content-evenly py-5 mt-5" v-if="recipes.length > 0">
      <RecipeCard :routeName="route.name" v-for="r in recipes" :key="r.id" :recipe="r" />
    </div>
    <div class="d-flex flex-column flex-grow-1" v-else>
      <span class="fadeIn m-auto fs-1 fw-700">Sorry, you have favorited no recipes!</span>
    </div>
  </section>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import RecipeCard from "../components/RecipeCard.vue";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";

export default {
    setup() {
        async function getFavoriteRecipes() {
            try {
                await recipesService.getFavoriteRecipes();
            }
            catch (error) {
                Pop.error(error.message, "[getFavoriteRecipes]");
            }
        }
        onMounted(() => {
            getFavoriteRecipes();
        });
        const route = useRoute()
        return {
          route,
          recipes: computed(() => {
              const filterByCategory = AppState.recipes.filter(recipe => recipe.category.toUpperCase().includes(AppState.search.toUpperCase()))
              const filterByTitle = AppState.recipes.filter(recipe => recipe.title.toUpperCase().includes(AppState.search.toUpperCase()))
              const filterBySubtitle = AppState.recipes.filter(recipe => recipe.subtitle.toUpperCase().includes(AppState.search.toUpperCase()))
              const finalSearchFilter = [...new Set([...filterByCategory, ...filterByTitle, ...filterBySubtitle])]
              return finalSearchFilter
            })
        };
    },
    components: { RecipeCard }
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
    animation: fadeIn ease 8s;
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