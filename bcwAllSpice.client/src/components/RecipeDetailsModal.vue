<template>
  <div class="modal fade" :id="'recipeDetailsModal' + recipe.id" tabindex="-1" :aria-labelledby="'recipeDetailsModalLabel' + recipe.id" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" :id="'recipeDetailsModalLabel' + recipe.id">{{recipe.title}}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          {{recipe.instructions}}
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
import { Recipe } from "../models/Recipe";

export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup() {

    return {

    }
  }
}
</script>

<style scoped lang="scss">

</style>