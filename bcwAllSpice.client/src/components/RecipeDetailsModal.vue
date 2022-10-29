<template>
  <div class="modal fade" :id="'recipeDetailsModal' + recipe.id" tabindex="-1"
    :aria-labelledby="'recipeDetailsModalLabel' + recipe.id" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" :id="'recipeDetailsModalLabel' + recipe.id">{{ recipe.title }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="d-flex justify-content-evenly gap-3">
            <div class="pos-relative">
              <img :src="recipe.img" :alt="recipe.title">
              <i class="mdi mdi-heart-outline heart selectable rounded fs-1 text-visible" type="button"
                @click="toggleFavorite()" v-if="!isFave()"></i>
              <i class="mdi mdi-heart heart selectable rounded favorite fs-1 text-visible" type="button"
                @click="toggleFavorite()" v-else></i>
            </div>
            <div class="d-flex flex-column">
              <div>
                <h3>{{ recipe.title }}</h3>
                <h6>{{ recipe.subtitle }}</h6>
              </div>
              <div class="d-flex gap-5 flex-grow-1 py-3">
                <div class="d-flex flex-column">
                  <span>RECIPE INSTRUCTIONS</span>
                  <div class="flex-grow-1 auto-scroll pt-3">{{ recipe.instructions }}</div>
                </div>
                <div class="d-flex flex-column">
                  <span>RECIPE INGREDIENTS</span>
                  <div class="ingredients auto-scroll pt-3">
                    <span v-for="i in ingredients">{{ i.quantity }}: {{ i.name }}</span>
                  </div>
                </div>
              </div>
              <div class="w-100">
                <div class="d-flex justify-content-end">
                  <span>created by someone</span>
                </div>
              </div>
            </div>
          </div>

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";

export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients),
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
    }
  }
}
</script>

<style scoped lang="scss">
.pos-relative {
  position: relative;
}

.heart {
  position: absolute;
  right: 2%;
  top: 2%;
}

.favorite {
  color: fuchsia;
}

img {
  width: 30vw;
}

.auto-scroll {
  overflow-y: auto;
}
</style>