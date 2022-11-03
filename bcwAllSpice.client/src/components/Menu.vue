<template>
  <div class="menu d-flex justify-content-center">
    <div class="menu-box px-3 py-2 rounded elevation-2 d-flex align-items-center gap-3">
      <button class="" @click="changePage(previous)" :disabled="hasPrevious"><span class="fs-3 rounded px-1">&#60;</span></button>
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
      <button class="" @click="changePage(next)" :disabled="!hasNext"><span class="fs-3 rounded px-1">&#62;</span></button>
      
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";

export default {
  setup() {
    const route = useRoute()
    return {
      route,
      previous: computed(() => AppState.previous),
      next: computed(() => AppState.next),
      hasPrevious: computed(() => AppState.hasPrevious),
      hasNext: computed(() => AppState.hasNext),
      async changePage(direction) {
        recipesService.changePage(direction, this.route.name)
      }
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

span {
  color: red;
  user-select: none;
  transition: 0.3s ease-in-out;
}

button {
  background-color: transparent;
  border: none;
}

button:hover {
  cursor: pointer;
}

button[disabled] {
  cursor: auto;
}

button[disabled] > span {
  color: transparent;
}
</style>