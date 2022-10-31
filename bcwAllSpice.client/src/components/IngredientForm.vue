<template>
  <form @submit.prevent="addIngredient()">
    <div class="input-group">
      <div class="form-floating">
        <input class="form-control" type="text" id="floatingQuantity" placeholder="Quantity" v-model="editable.quantity"> 
        <label for="floatingQuantity">Quantity</label>
      </div>
      <div class="form-floating">
        <input class="form-control" type="text" id="floatingName" placeholder="Name" v-model="editable.name"> 
        <label for="floatingName">Name</label>
      </div>
      <button class="btn btn-outline-dark" title="Add Instructions"><i class="mdi mdi-plus-outline selectable fs-3"></i></button>
    </div>
  </form>
</template>

<script>
import { ref } from "vue";
import { ingredientsService } from "../services/IngredientsService";
import Pop from "../utils/Pop";

export default {
  props: {
    recipeId: { type: Number, required: true }
  },
  setup(props) {
    const editable = ref({})
    return {
      editable,
      async addIngredient() {
        try {
          await ingredientsService.addIngredient(editable.value, props.recipeId);
          editable.value = {}
        }
        catch(error) {
          Pop.error(error.message, "[addIngredient]")
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">

</style>