<template>
  <form id="createRecipeForm" @submit.prevent="handleSubmit()">
    <div class="d-flex gap-3 flex-wrap mb-3">

      <div class="form-floating flex-grow-1">
        <input class="form-control" id="floatingTitle" type="text" placeholder="Title" required v-model="editable.title">
        <label for="floatingTitle">Title</label>
      </div>
      <div class="form-floating flex-grow-1">
        <select class="form-select" id="floatingSelect" aria-label="Select a Category" required v-model="editable.category">
          <option value="Breakfast">Breakfast</option>
          <option value="Lunch">Lunch</option>
          <option value="3Dinner">Dinner</option>
        </select>
        <label for="floatingSelect">Category</label>
      </div>
    </div>
      <div class="form-floating">
        <input class="form-control" id="floatingSubTitle" type="text" placeholder="Sub Title" v-model="editable.subtitle">
        <label for="floatingSubTitle">Sub Title</label>
      </div>
    <div class="subtext d-flex justify-content-between align-items-center px-2">
      <span>A brief description of the recipe</span>
      <span>{{editable.subtitle ? editable.subtitle.length : 0}}/50</span>
    </div>
  </form>
</template>

<script>
import { ref } from "vue";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          console.log("handling submit", editable.value)
          await recipesService.createRecipe(editable.value)
        }
        catch(error) {
          Pop.error(error.message, "[createRecipe]")
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
  .subtext {
    color: gray;
  }
</style>