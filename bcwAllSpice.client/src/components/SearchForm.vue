<template>
  <form @submit.prevent="handleSubmit()">
    <div class="input-group">
      <input type="text" class="form-control" placeholder="Search...">
      <button class="btn btn-info" type="submit"><i class="mdi mdi-magnify"></i></button>
    </div>
  </form>
</template>

<script>
export default {
  setup() {
    
    return {
      
    }
  }
}
</script>

<style scoped lang="scss">
  
</style>