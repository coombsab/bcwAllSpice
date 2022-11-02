<template>
  <form @submit.prevent="updateAccount()">
    <div class="form-floating mb-3">
      <input type="text" class="form-control" name="floatingName" placeholder="Change Name" v-model="editable.name">
      <label for="floatingName">Change Name</label>
    </div>
    <div class="form-floating mb-3">
      <input type="url" class="form-control" name="floatingPicture" placeholder="Change Picture" v-model="editable.picture">
      <label for="floatingPicture">Change Picture</label>
    </div>
    <button type="submit" class="btn btn-outline-dark">Save Changes</button>
  </form>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref, watchEffect } from "vue";
import { AppState } from "../AppState";
import { accountService } from "../services/AccountService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      account: computed(() => AppState.account),
      async updateAccount() {
        try {
          await accountService.updateAccount(editable.value)
        }
        catch(error) {
          Pop.error(error.message, "[updateAccount]")
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
  
</style>