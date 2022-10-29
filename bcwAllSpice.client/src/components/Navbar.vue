<template>
  <div class="nav d-flex justify-content-end align-items-start p-3 gap-3">
    <SearchForm />
    <Login />
    <Menu />
  </div>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
.nav {
  height: 20vh;
  background-image: url("https://images.unsplash.com/photo-1466637574441-749b8f19452f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1480&q=80");
  background-size: cover;
  background-position: center;
  position: relative;
}
</style>
