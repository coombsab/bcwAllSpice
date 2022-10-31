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
              <div class="edit-icon-div d-flex align-items-center">
                <i class="mdi mdi-square-edit-outline selectable fs-1 edit-icon" @click="toggleVisibility('imgForm')"
                  v-show="!isEditImageVisible"></i>
                <form @submit.prevent="editRecipe('imgForm')" id="imgForm" v-show="isEditImageVisible">
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
                  <h3 v-show="!isEditTitleVisible">{{ recipe.title }} <i class="mdi mdi-square-edit-outline selectable"
                      title="Edit Title" @click="toggleVisibility('titleForm')"></i></h3>
                  <form @submit.prevent="editRecipe('titleForm')" id="titleForm" v-show="isEditTitleVisible">
                    <div class="form-floating">
                      <input type="text" id="editTitle" class="form-control" placeholder="Edit Title"
                        v-model="editable.title" onfocus="select()">
                      <label for="editTitle">Edit Title</label>
                    </div>
                  </form>
                  <h6 v-show="!isEditSubtitleVisible">{{ recipe.subtitle === "" ? 'Add subtitle' : recipe.subtitle }} <i
                      class="mdi mdi-square-edit-outline selectable" title="Edit Subtitle" v-if="recipe.subtitle !== ''"
                      @click="toggleVisibility('subtitleForm')"></i><i class="mdi mdi-plus-outline selectable"
                      title="Add Subtitle" @click="toggleVisibility('subtitleForm')" v-else></i></h6>
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
                  <span>RECIPE INSTRUCTIONS</span>
                  <div class="flex-grow-1 auto-scroll pt-3" v-show="!isEditInstructionsVisible"><span>{{
                      !recipe.instructions ? "Add instructions" :
                        recipe.instructions
                  }}</span> <i class="mdi mdi-square-edit-outline selectable" title="Edit Instructions"
                      v-if="recipe.instructions" @click="toggleVisibility('instructionsForm')"></i><i
                      class="mdi mdi-plus-outline selectable" title="Add Instructions"
                      @click="toggleVisibility('instructionsForm')" v-else></i>
                  </div>
                  <form @submit.prevent="editRecipe('instructionsForm')" id="instructionsForm" v-show="isEditInstructionsVisible">
                    <div class="form-floating">
                      <textarea id="editInstructions" class="form-control" placeholder="Edit Instructions"
                        v-model="editable.instructions" onfocus="select()"></textarea>
                      <label for="editInstructions">Edit Instructions</label>
                    </div>
                    <button type="submit">Save Changes</button>
                  </form>
                </div>
                <div class="d-flex flex-column width-ingredients">
                  <span>RECIPE INGREDIENTS</span>
                  <div class="ingredients auto-scroll pt-3">
                    <IngredientCard v-for="i in ingredients" :key="i.id" :ingredient="i" />
                    <IngredientForm :recipeId="recipe.id" />
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
            isEditTitleVisible: computed(() => AppState.isEditTitleVisible),
            isEditSubtitleVisible: computed(() => AppState.isEditSubtitleVisible),
            isEditImageVisible: computed(() => AppState.isEditImageVisible),
            isEditInstructionsVisible: computed(() => AppState.isEditInstructionsVisible),
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
  color: rgba(0, 0, 0, 0);
  transition: 0.2s linear;
}

.pos-relative:hover .edit-icon {
  text-shadow: 2px 0 2px rgba(0, 0, 0, 0.644), 0 2px 2px rgba(0, 0, 0, 0.644), -2px 0 2px rgba(0, 0, 0, 0.644), 0 -2px 2px rgba(0, 0, 0, 0.644), 0 0 10px rgba(255, 4, 4, 0.846);
  color: rgb(241, 231, 231);
  font-weight: 700;
  letter-spacing: 0.035rem;
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

#editInstructions {
  height: 32.4rem;
}

.width-instructions {
  width: 60%;

}

.width-ingredients {
  width: 40%;

}

.ingredients {
  display: flex;
  flex-direction: column;
}
</style>