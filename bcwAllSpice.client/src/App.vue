<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <div class="dot selectable elevation-3" type="button" data-bs-toggle="modal" data-bs-target="#createRecipeModal">
      <i class="mdi mdi-plus fs-1 " title="Add Recipe"></i>
    </div>
  </main>
  <CreateRecipeModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateRecipeModal from "./components/CreateRecipeModal.vue"
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState),
    }
  },
  components: { Navbar, CreateRecipeModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  position: relative;
}

.circle {
  border-radius: 50%;
}

.dot {
  border-radius: 50%;
  height: 3rem;
  width: 3rem;
  background-color: rgb(90, 89, 89);
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  bottom: 1%;
  right: 1%;
  color: whitesmoke;
  font-weight: 800;
}
</style>
