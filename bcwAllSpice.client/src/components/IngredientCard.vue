<template>
  <div class="mb-1 d-flex gap-2 flex-wrap justify-content-between ingredient">
    <span>{{ ingredient.quantity.toUpperCase() }}: {{ ingredient.name.toUpperCase() }}</span>
    <i class="mdi mdi-delete selectable on-hover" title="Delete Ingredient" @click="deleteIngredient()"></i>
  </div>

</template>

<script>
import { computed } from "@vue/reactivity";
import { ref, watchEffect } from "vue";
import { AppState } from "../AppState";
import { Ingredient } from "../models/Ingredient";
import { ingredientsService } from "../services/IngredientsService";
import Pop from "../utils/Pop";

export default {
  props: {
    ingredient: { type: Ingredient }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.ingredient };
    });
    return {
      editable,
      async editIngredient(elementId) {
        try {
          await ingredientsService.editIngredient(editable.value, props.ingredient.id)
        }
        catch (error) {
          Pop.error(error.message, "[editIngredient]")
        }
      },
      async deleteIngredient() {
        try {
          const yes = await Pop.confirm(`Delete ${props.ingredient.quantity}: ${props.ingredient.name}`)
          if (!yes) {
            return
          }
          await ingredientsService.deleteIngredient(props.ingredient.id)
        }
        catch (error) {
          Pop.error(error.message, "[deleteIngredient]")
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.ingredient:hover {
  background-color: rgba(10, 10, 10, 0.15);
  border-radius: 0.5rem;
  padding: 0.25rem;
}
</style>