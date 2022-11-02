<template>
  <div class="about d-flex flex-column align-items-center pt-5">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <div class="edits">
      <EditAccountForm />
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import EditAccountForm from "../components/EditAccountForm.vue";
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: { EditAccountForm }
}
</script>

<style scoped>
img {
  max-width: 300px;
}

.edits {
  width: 90%;
}

@media (min-width: 768px) {
  .edits {
    width: 50%;
  }
}
</style>
