<template>
  <form id="createRecipeForm" @submit.prevent="handleSubmit()">
    <div class="d-flex gap-3 flex-wrap mb-3">

      <div class="form-floating flex-grow-1">
        <input class="form-control" id="floatingTitle" type="text" placeholder="Title" required v-model="editable.title"
          onfocus="this.select()">
        <label for="floatingTitle">Title</label>
      </div>
      <div class="form-floating flex-grow-1">
        <select class="form-select" id="floatingSelect" aria-label="Select a Category" required
          v-model="editable.category">
          <option value="Breakfast">Breakfast</option>
          <option value="Lunch">Lunch</option>
          <option value="Dinner">Dinner</option>
        </select>
        <label for="floatingSelect">Category</label>
      </div>
    </div>
    <div class="form-floating">
      <input class="form-control" id="floatingSubtitle" type="text" placeholder="Sub Title" v-model="editable.subtitle"
        onfocus="this.select()">
      <label for="floatingSubtitle">Sub Title</label>
    </div>
    <div class="subtext d-flex justify-content-between align-items-center px-2 mb-3">
      <span>A brief description of the recipe</span>
      <span>{{ editable.subtitle ? editable.subtitle.length : 0 }}/50</span>
    </div>
    <div class="form-floating mb-3">
      <input class="form-control" id="floatingImage" type="url" placeholder="Image URL" v-model="editable.img"
        onfocus="this.select()">
      <label for="floatingImage">Image URL</label>
    </div>
    <div class="form-floating">
      <textarea class="form-control textarea-height" type="text" id="floatingInstructions" placeholder="Instructions"
        v-model="editable.instructions" onfocus="this.select()"></textarea>
      <label for="floatingInstructions">Instructions</label>
    </div>

  </form>
</template>

<script>
import { Modal } from "bootstrap";
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
          // console.log("handling submit", editable.value)
          await recipesService.createRecipe(editable.value)
          Modal.getOrCreateInstance(document.getElementById('createRecipeModal')).hide()
          editable.value = {}
        }
        catch (error) {
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

#floatingInstructions {
  height: 10.8rem;
}
</style>