<template>
  <div class="menu d-flex justify-content-center">
    <div class="menu-box px-5 py-2 rounded elevation-2">
      <div class="d-flex gap-5 justify-content-center align-items-center fs-4">
        <router-link :to="{ name: 'Home' }">
          <span>Home</span>
        </router-link>
        <router-link :to="{ name: 'MyRecipes' }">
          <span>My Recipes</span>
        </router-link>
        <router-link :to="{ name: 'Favorites' }">
          <span>Favorites</span>
        </router-link>
      </div>
    </div>
  </div>
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
  .menu {
    position: absolute;
    bottom: -15%;
    right: 0%;
    width: 100%;
  }
  
  .menu-box {
    background-color: white;
    width: fit-content;
    z-index: 1;
  }

</style>