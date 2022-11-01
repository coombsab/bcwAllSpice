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
import { searchService } from "../services/SearchService.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      handleSubmit() {
        if (!editable.value.search) {
          editable.value.search = ""
        }
        searchService.saveSearch(editable.value.search)
      }
    }
  }
}
</script>

<style scoped lang="scss">
  
</style>