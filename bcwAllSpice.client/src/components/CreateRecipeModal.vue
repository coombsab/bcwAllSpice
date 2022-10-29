<template>
  <div class="modal fade" id="createRecipeModal" tabindex="-1" aria-labelledby="createRecipeModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="createRecipeModalLabel">New Recipe</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <CreateRecipeForm />
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="submit" form="createRecipeForm" class="btn btn-primary">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CreateRecipeForm from "./CreateRecipeForm.vue";

export default {
    setup() {
        return {
          
        };
    },
    components: { CreateRecipeForm }
}
</script>

<style scoped lang="scss">

</style>