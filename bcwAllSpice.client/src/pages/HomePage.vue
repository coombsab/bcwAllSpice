<template>
  <section id="home" class="home">
    <div class="home-content d-flex flex-wrap gap-3 px-3 justify-content-evenly py-5 mt-5" v-if="recipes.length > 0">
      <RecipeCard v-for="r in recipes" :key="r.id" :recipe="r" />
    </div>
    <div class="d-flex flex-column flex-grow-1 pos-relative" v-else>
      <span class="fadeIn m-auto fs-1 fw-700 px-5">No recipes currently available, refresh the page (server may have
        timed out) or add some, please!</span>
      <Spinner />
    </div>
  </section>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import RecipeCard from "../components/RecipeCard.vue";
import Spinner from "../components/Spinner.vue";
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
  components: { RecipeCard, Spinner }
}
</script>

<style scoped lang="scss">
.pos-relative {
  position: relative;
}

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
  animation: fadeIn ease 11s;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }

  75% {
    opacity: 0;
  }

  100% {
    opacity: 1;
  }
}
</style>
