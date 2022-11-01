<template>
  <div class="mb-1 d-flex justify-content-between ingredient align-items-center">
    <span>{{ ingredient.quantity.toUpperCase() }}: {{ ingredient.name.toUpperCase() }}</span>
    <i class="mdi mdi-delete selectable delete-icon" :title="'Delete ' + ingredient.quantity + ': ' + ingredient.name" @click="deleteIngredient()"></i>
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
@media (min-width: 768px) {
  .delete-icon {
    color: rgba(10, 10, 10, 0);
  }

  .ingredient:hover .delete-icon {
    color: rgb(10, 10, 10);
    transition: 0.3s linear;
  }

  .ingredient:hover {
    background-color: rgba(10, 10, 10, 0.15);
    border-radius: 0.5rem;
    padding: 0 0.25rem;
  }
}
</style>