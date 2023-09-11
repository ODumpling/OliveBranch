<script>
export default {
  layout: null,
}
</script>
<script setup>
import {useForm} from "@inertiajs/vue3";
import Toastr from "../../Components/Toastr.vue";

const form = useForm({
  username: '',
  email: '',
  password: '',
  confirmPassword: '',
});
const submit = () => {
  form.post('/auth/register', {
    onFinish: () => form.reset('password', 'confirmPassword'),
  });
};

defineProps({
    errors: Object
})
</script>

<template>
  <section class="flex items-center flex-1 w-full overflow-x-hidden min-h-screen">

    <div class="w-full max-w-sm mx-auto overflow-hidden bg-white rounded-lg shadow-md dark:bg-gray-800">
      <div class="px-6 py-4">
        <p class="mt-1 text-center text-gray-500 dark:text-gray-400">Create account</p>

        <form @submit.prevent="submit">
          <div class="w-full mt-4">
            <input
                v-model="form.username"
                class="block w-full px-4 py-2 mt-2 text-gray-700 dark:text-white placeholder-gray-500 bg-white border rounded-lg dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300"
                type="text" placeholder="Username" aria-label="Username" autocomplete="username"/>
          </div>

          <div class="w-full mt-4">
            <input
                v-model="form.email"
                class="block w-full px-4 py-2 mt-2 text-gray-700 dark:text-white placeholder-gray-500 bg-white border rounded-lg dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300"
                type="email" placeholder="Email Address" aria-label="Email Address" autocomplete="email"/>
          </div>

          <div class="w-full mt-4">
            <input
                v-model="form.password"
                class="block w-full px-4 py-2 mt-2 text-gray-700 dark:text-white placeholder-gray-500 bg-white border rounded-lg dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300"
                type="password" placeholder="Password" aria-label="Password" autocomplete="new-password"/>
          </div> 
          <div class="w-full mt-4">
            <input
                v-model="form.confirmPassword"
                class="block w-full px-4 py-2 mt-2 text-gray-700 dark:text-white placeholder-gray-500 bg-white border rounded-lg dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300"
                type="password" placeholder="Confirm Password" aria-label="ConfirmPassword" autocomplete="new-password"/>
          </div>

          <div class="flex items-center justify-between mt-4">
            <Link href="/auth/login" class="text-sm text-gray-600 dark:text-gray-200 hover:text-gray-500">Go Back</Link>

            <button
                class="px-6 py-2 text-sm font-medium tracking-wide text-white capitalize transition-colors duration-300 transform bg-blue-500 rounded-lg hover:bg-blue-400 focus:outline-none focus:ring focus:ring-blue-300 focus:ring-opacity-50">
              Sign In
            </button>
          </div>
        </form>
      </div>
    </div>
    <Toastr :errors="errors" v-if="Object.keys(errors).length > 1">
      Ensure that these requirements are met:
    </Toastr>
  </section>

</template>