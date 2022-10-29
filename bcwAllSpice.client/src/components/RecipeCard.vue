<template>
  <div class="recipe-card text-visible elevation-2" :style="{ backgroundImage: `url(${recipe.img})` }">
    <div class="layer selectable p-3" data-bs-toggle="modal" :data-bs-target="'#recipeDetailsModal' + recipe.id" >
      <div class="card-content">
        <div class="d-flex align-items-center">
          <span>{{ recipe.category }}</span>
        </div>
        <span>{{ recipe.title }}</span>
      </div>
    </div>
    <i class="mdi mdi-heart heart selectable rounded" type="button" @click="toggleFavorite()"></i>
  </div>
  <RecipeDetailsModal :recipe="recipe" />
</template>

<script>
import { Recipe } from "../models/Recipe";
import RecipeDetailsModal from "./RecipeDetailsModal.vue";

export default {
    props: {
        recipe: { type: Recipe, required: true }
    },
    setup(props) {
        return {
          async toggleFavorite () {
            console.log("toggling favs")
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

.card-content {
  display: flex;
  flex-direction: column;
  height: 100%;
  justify-content: space-between;
}
</style>