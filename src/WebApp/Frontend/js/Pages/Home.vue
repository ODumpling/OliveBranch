<template>
  <div class="max-w-full px-8 py-4 bg-white rounded-lg shadow-md dark:bg-gray-800 m-8">
    <span class="text-gray-700 dark:text-white">Home Page</span>
  </div>
</template>
<script>
import {Link} from '@inertiajs/vue3'

export default {
  components: {Link},
  props: {
    name: String,
  }
}
</script>