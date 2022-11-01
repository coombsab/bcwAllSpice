<template>
  <div class="modal fade" :id="'recipeDetailsModal' + recipe.id" tabindex="-1"
    :aria-labelledby="'recipeDetailsModalLabel' + recipe.id" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-fullscreen-md-down elevation-3">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="d-flex flex-wrap flex-md-nowrap justify-content-between gap-3">
            <div class="pos-relative">
              <div class="image" :style="{ backgroundImage: `url(${recipe.img})` }"></div>
              <div class="heart" v-if="routeName != 'Favorites' && user.isAuthenticated">
                <i class="mdi mdi-heart-outline selectable rounded fs-1 text-visible" type="button"
                  @click="toggleFavorite()" v-if="!isFave()" title="Favorite Recipe"></i>
                <i class="mdi mdi-heart selectable rounded favorite fs-1 text-visible" type="button"
                  @click="toggleFavorite()" v-else title="Unfavorite Recipe"></i>
              </div>
              <div class="edit-icon-div d-flex align-items-center" v-if="recipe.creatorId === account.id">
                <i class="mdi mdi-square-edit-outline selectable fs-1 edit-icon" @click="toggleVisibility('imgForm')"
                  v-show="!isEditImageVisible" title="Edit Image"></i>
                <form @submit.prevent="editRecipe('imgForm')" id="imgForm" v-show="isEditImageVisible">
                  <div class="form-floating">
                    <input type="url" id="editImg" class="form-control" placeholder="Image URL" v-model="editable.img"
                      onfocus="select()">
                    <label for="editImg">Image URL</label>
                  </div>
                </form>
              </div>
            </div>
            <div class="d-flex flex-column w-100 p-3">
              <div class="d-flex justify-content-between">
                <div>
                  <div class="d-flex gap-2 align-items-center flex-wrap">
                    <div>
                      <h3 v-show="!isEditTitleVisible" class="m-0 mb-md-2">{{ recipe.title }} <i
                          class="mdi mdi-square-edit-outline selectable" title="Edit Title"
                          @click="toggleVisibility('titleForm')" v-if="recipe.creatorId === account.id"></i></h3>
                      <form @submit.prevent="editRecipe('titleForm')" id="titleForm" v-show="isEditTitleVisible">
                        <div class="form-floating">
                          <input type="text" id="editTitle" class="form-control" placeholder="Edit Title"
                            v-model="editable.title" onfocus="select()">
                          <label for="editTitle">Edit Title</label>
                        </div>
                      </form>
                    </div>
                    <div>
                      <p class="m-0 mb-2" v-show="!isEditCategoryVisible">({{ recipe.category }}) <i
                          class="mdi mdi-square-edit-outline selectable" title="Edit Category"
                          @click="toggleVisibility('categoryForm')" v-if="recipe.creatorId === account.id"></i></p>
                      <form @submit.prevent="editRecipe('categoryForm')" id="categoryForm"
                        v-show="isEditCategoryVisible">
                        <div class="input-group">
                          <div class="form-floating flex-grow-1">
                            <select class="form-select" id="editCategory" aria-label="Select a Category" required
                              v-model="editable.category">
                              <option v-for="c in categories" :value="c">{{ c }}</option>
                            </select>
                            <label for="editCategory">Category</label>
                          </div>
                          <button class="btn btn-outline-dark"><i class="mdi mdi-plus-outline"
                              title="Submit Category Change"></i></button>
                        </div>
                      </form>
                    </div>
                  </div>
                  <h6 v-show="!isEditSubtitleVisible">{{ recipe.subtitle === "" ? 'Add subtitle' : recipe.subtitle }} <span v-if="recipe.creatorId === account.id"><i
                      class="mdi mdi-square-edit-outline selectable" title="Edit Subtitle" v-if="recipe.subtitle !== ''"
                      @click="toggleVisibility('subtitleForm')"></i><i class="mdi mdi-plus-outline selectable"
                      title="Add Subtitle" @click="toggleVisibility('subtitleForm')" v-else></i></span></h6>
                  <form @submit.prevent="editRecipe('subtitleForm')" id="subtitleForm" v-show="isEditSubtitleVisible">
                    <div class="form-floating">
                      <input type="text" id="editSubtitle" class="form-control" placeholder="Edit Subtitle"
                        v-model="editable.subtitle" onfocus="select()">
                      <label for="editSubtitle">Edit Subtitle</label>
                    </div>
                  </form>
                </div>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="d-flex gap-5 flex-grow-1 py-3">
                <div class="d-flex flex-column width-instructions">
                  <span><strong>RECIPE INSTRUCTIONS</strong></span>
                  <div class="flex-grow-1 auto-scroll pt-3" v-show="!isEditInstructionsVisible"><span>{{
                      !recipe.instructions ? "Add instructions" :
                        recipe.instructions
                  }}</span> <span v-if="recipe.creatorId === account.id"><i class="mdi mdi-square-edit-outline selectable" title="Edit Instructions"
                      v-if="recipe.instructions" @click="toggleVisibility('instructionsForm')"></i><i
                      class="mdi mdi-plus-outline selectable" title="Add Instructions"
                      @click="toggleVisibility('instructionsForm')" v-else></i></span>
                  </div>
                  <form @submit.prevent="editRecipe('instructionsForm')" id="instructionsForm"
                    v-show="isEditInstructionsVisible">
                    <div class="form-floating">
                      <textarea id="editInstructions" class="form-control" placeholder="Edit Instructions"
                        v-model="editable.instructions" onfocus="select()"></textarea>
                      <label for="editInstructions">Edit Instructions</label>
                    </div>
                    <button type="submit" class="btn btn-outline-dark">Save Changes</button>
                  </form>
                </div>
                <div class="d-flex flex-column width-ingredients">
                  <span><strong>RECIPE INGREDIENTS</strong></span>
                  <div class="ingredients pt-3">
                    <div class="d-flex flex-column flex-grow-1 auto-scroll">
                      <IngredientCard v-for="i in ingredients" :key="i.id" :ingredient="i" />
                    </div>
                    <IngredientForm :recipeId="recipe.id" v-if="recipe.creatorId === account.id" />
                  </div>
                </div>
              </div>
              <div class="w-100">
                <div class="d-flex justify-content-between align-items-center">
                  <button type="button" class="btn btn-danger" @click="deleteRecipe()" v-if="recipe.creatorId === account.id">Delete Recipe</button>
                  <div v-else></div>
                  <span class="text-end">created by {{ recipe.creator.name }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { ref, onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";
import IngredientCard from "./IngredientCard.vue";

export default {
  props: {
    recipe: { type: Recipe, required: true },
    routeName: { type: String }
  },
  setup(props) {
    const route = useRoute();
    const editable = ref({});
    watchEffect(() => {
      editable.value = { ...props.recipe };
    });
    return {
      route,
      editable,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      categories: computed(() => AppState.categories.sort()),
      isEditTitleVisible: computed(() => AppState.isEditTitleVisible),
      isEditSubtitleVisible: computed(() => AppState.isEditSubtitleVisible),
      isEditImageVisible: computed(() => AppState.isEditImageVisible),
      isEditInstructionsVisible: computed(() => AppState.isEditInstructionsVisible),
      isEditCategoryVisible: computed(() => AppState.isEditCategoryVisible),
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients),
      async toggleFavorite() {
        try {
          await recipesService.toggleFavorite(props.recipe.id);
        }
        catch (error) {
          Pop.error(error.message, "[toggleFavorite]");
        }
      },
      isFave() {
        if (props.recipe.favoriteeIds.find(id => id === this.account.id)) {
          return true;
        }
        else {
          return false;
        }
      },
      async editRecipe(elementId) {
        try {
          // console.log("editing recipe from", elementId)
          editable.value.id = props.recipe.id;
          await recipesService.editRecipe(editable.value);
          this.toggleVisibility(elementId);
          // editable.value = {}
        }
        catch (error) {
          Pop.error(error.message, "[editRecipe]");
        }
      },
      async deleteRecipe() {
        try {
          const yes = await Pop.confirm(`Delete ${props.recipe.title}?`)
          if (!yes) {
            return
          }
          Modal.getOrCreateInstance(document.getElementById('recipeDetailsModal' + props.recipe.id)).hide()
          await recipesService.deleteRecipe(props.recipe.id)

        }
        catch (error) {
          Pop.error(error.message, "[deleteRecipe]")
        }
      },
      toggleVisibility(elementId) {
        // console.log("unhiding", elementId)
        switch (elementId) {
          case "titleForm":
            AppState.isEditTitleVisible = !AppState.isEditTitleVisible;
            document.getElementById(elementId).focus();
            break;
          case "subtitleForm":
            AppState.isEditSubtitleVisible = !AppState.isEditSubtitleVisible;
            document.getElementById(elementId).focus();
            break;
          case "imgForm":
            AppState.isEditImageVisible = !AppState.isEditImageVisible;
            document.getElementById(elementId).focus();
            break;
          case "instructionsForm":
            AppState.isEditInstructionsVisible = !AppState.isEditInstructionsVisible;
            document.getElementById(elementId).focus();
            break;
          case "categoryForm":
            AppState.isEditCategoryVisible = !AppState.isEditCategoryVisible;
            document.getElementById(elementId).focus();
            break;
        }
      }
    };
  },
  components: { IngredientCard }
}
</script>

<style scoped lang="scss">
.pos-relative {
  position: relative;
  width: 100%;
}

.heart {
  position: absolute;
  right: 2%;
  top: 2%;
}

.edit-icon-div {
  position: absolute;
  left: 2%;
  top: 2%;
}

.edit-icon {
  text-shadow: 2px 0 2px rgba(0, 0, 0, 0.644), 0 2px 2px rgba(0, 0, 0, 0.644), -2px 0 2px rgba(0, 0, 0, 0.644), 0 -2px 2px rgba(0, 0, 0, 0.644), 0 0 10px rgba(255, 4, 4, 0.846);
  color: rgb(241, 231, 231);
  font-weight: 700;
  letter-spacing: 0.035rem;
}


.favorite {
  color: fuchsia;
}

.auto-scroll {
  overflow-y: auto;
}

.image {
  height: 60vh;
  width: 100%;
  background-position: center;
  background-size: cover;
}

#editInstructions {
  height: 32.4rem;
}

.ingredients {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.width-instructions {
  width: 50%;

}

.width-ingredients {
  width: 50%;

}

@media (min-width: 768px) {
  .width-instructions {
    width: 60%;

  }

  .width-ingredients {
    width: 40%;

  }

  .pos-relative {
    width: 50%;
  }

  .image {
    border-radius: 0.375rem 0 0 0.375rem;
  }

  .pos-relative:hover .edit-icon {
    text-shadow: 2px 0 2px rgba(0, 0, 0, 0.644), 0 2px 2px rgba(0, 0, 0, 0.644), -2px 0 2px rgba(0, 0, 0, 0.644), 0 -2px 2px rgba(0, 0, 0, 0.644), 0 0 10px rgba(255, 4, 4, 0.846);
    color: rgb(241, 231, 231);
    font-weight: 700;
    letter-spacing: 0.035rem;
  }

  .edit-icon {
    color: rgba(0, 0, 0, 0);
    transition: 0.2s linear;
    text-shadow: none;
  }
}
</style>