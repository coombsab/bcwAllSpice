<template>
  <div class="modal fade" :id="'recipeDetailsModal' + recipe.id" tabindex="-1"
    :aria-labelledby="'recipeDetailsModalLabel' + recipe.id" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="d-flex justify-content-between gap-3">
            <div class="pos-relative">
              <img :src="recipe.img" :alt="recipe.title">
              <div class="heart" v-if="routeName != 'Favorites'">
                <i class="mdi mdi-heart-outline selectable rounded fs-1 text-visible" type="button"
                  @click="toggleFavorite()" v-if="!isFave()"></i>
                <i class="mdi mdi-heart selectable rounded favorite fs-1 text-visible" type="button"
                  @click="toggleFavorite()" v-else></i>
              </div>
              <div class="edit-icon d-flex align-items-center on-hover">
                <i class="mdi mdi-square-edit-outline text-visible selectable fs-1"
                  @click="toggleVisibility('imgForm')"></i>
                <form @submit.prevent="editRecipe('imgForm')" id="imgForm">
                  <div class="form-floating">
                    <input type="url" id="editImg" class="form-control" placeholder="Image URL" v-model="editable.img"
                      onfocus="select()">
                    <label for="editImg">Image URL</label>
                  </div>
                </form>
              </div>
            </div>
            <div class="d-flex flex-column w-100">
              <div class="d-flex justify-content-between">
                <div>
                  <h3 v-show="!isEditTitleVisible">{{ recipe.title }} <i class="mdi mdi-square-edit-outline selectable" title="Edit Title"
                      @click="toggleVisibility('titleForm')"></i></h3>
                  <form @submit.prevent="editRecipe('titleForm')" id="titleForm" v-show="isEditTitleVisible">
                    <div class="form-floating">
                      <input type="text" id="editTitle" class="form-control" placeholder="Edit Title" v-model="editable.title">
                      <label for="editTitle">Edit Title</label>
                    </div>
                  </form>
                  <h6>{{ recipe.subtitle === "" ? 'Add subtitle' : recipe.subtitle }} <i
                      class="mdi mdi-square-edit-outline selectable" title="Edit Subtitle"
                      v-if="recipe.subtitle !== ''"></i><i class="mdi mdi-plus-outline selectable"
                      title="Add Instructions" v-else></i></h6>
                </div>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="d-flex gap-5 flex-grow-1 py-3">
                <div class="d-flex flex-column width-instructions">
                  <span>RECIPE INSTRUCTIONS</span>
                  <div class="flex-grow-1 auto-scroll pt-3">{{ !recipe.instructions ? "Add instructions" :
                      recipe.instructions
                  }} <i class="mdi mdi-square-edit-outline selectable" title="Edit Instructions"
                      v-if="recipe.instructions"></i><i class="mdi mdi-plus-outline selectable" title="Add Instructions"
                      v-else></i></div>
                </div>
                <div class="d-flex flex-column width-ingredients">
                  <span>RECIPE INGREDIENTS</span>
                  <div class="ingredients auto-scroll pt-3">
                    <span v-for="i in ingredients">{{ i.quantity }}: {{ i.name }} <i
                        class="mdi mdi-square-edit-outline selectable" title="Edit Ingredient"></i> <i
                        class="mdi mdi-delete selectable" title="Delete Ingredient"></i></span>
                    <div><span>Add Ingredient</span> <i class="mdi mdi-plus-outline selectable"
                        title="Add Instructions"></i></div>
                  </div>
                </div>
              </div>
              <div class="w-100">
                <div class="d-flex justify-content-end">
                  <span>created by {{ recipe.creator.name }}</span>
                </div>
              </div>
            </div>
          </div>

        </div>
        <div class="modal-footer">
          <div class="d-flex justify-content-between align-items-center w-100">
            <button type="button" class="btn btn-danger">Delete Recipe</button>
            <div class="d-flex gap-3">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref, onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";

export default {
  props: {
    recipe: { type: Recipe, required: true },
    routeName: { type: String }
  },
  setup(props) {
    const route = useRoute()
    const editable = ref({})

    watchEffect(() => {
      editable.value = { ...props.recipe };
    });
    return {
      route,
      editable,
      isEditTitleVisible: computed(() => AppState.isEditTitleVisible),
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients),
      async toggleFavorite() {
        try {
          await recipesService.toggleFavorite(props.recipe.id)
        }
        catch (error) {
          Pop.error(error.message, "[toggleFavorite]")
        }
      },
      isFave() {
        if (props.recipe.favoriteeIds.find(id => id === this.account.id)) {
          return true
        } else {
          return false
        }
      },
      async editRecipe(elementId) {
        try {
          // console.log("editing recipe from", elementId)
          editable.value.id = props.recipe.id
          await recipesService.editRecipe(editable.value)
          this.toggleVisibility(elementId)
          // editable.value = {}
        }
        catch (error) {
          Pop.error(error.message, "[editRecipe]")
        }
      },
      toggleVisibility(elementId) {
        console.log("unhiding", elementId)
        AppState.isEditTitleVisible = !AppState.isEditTitleVisible
      }
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

.edit-icon {
  position: absolute;
  left: 2%;
  top: 2%;
}

.favorite {
  color: fuchsia;
}

img {
  max-width: 20vw;
}

.auto-scroll {
  overflow-y: auto;
}

img {
  max-height: 60vh;
}

.width-instructions {
  width: 60%;

}

.width-ingredients {
  width: 40%;

}
</style>