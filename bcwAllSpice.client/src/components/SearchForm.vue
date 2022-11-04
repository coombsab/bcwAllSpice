<template>
  <form @submit.prevent="handleSubmit()">
    <div class="input-group">
      <input type="text" class="form-control" placeholder="Search..." v-model="editable.search" onfocus="this.select()">
      <button class="btn btn-info" type="submit" title="Submit Search"><i class="mdi mdi-magnify"></i></button>
    </div>
  </form>
</template>

<script>
import { ref } from "vue";
import { useRoute } from "vue-router";
import { recipesService } from "../services/RecipesService";
import { searchService } from "../services/SearchService.js";

export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      handleSubmit() {
        if (!editable.value.search) {
          editable.value.search = ""
        }
        searchService.saveSearch(editable.value.search)
        switch(route.name) {
          case "Home":
            recipesService.getAllRecipes({ search: editable.value.search })
            break;
          case "MyRecipes":
            recipesService.getMyRecipes({ search: editable.value.search })
            break;
          case "Favorites":
            recipesService.getFavoriteRecipes({ search: editable.value.search })
            break;
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">

</style>