﻿<script>
export default {
  layout: null,
}
</script>

<script setup>

import {router, useForm} from "@inertiajs/vue3";
import Toastr from "../../Components/Toastr.vue";

const props = defineProps({
  errors: Object,
  returnUrl: String,
});


let form = useForm({
  email: '',
  password: '',
  rememberMe: false,
  returnUrl: props.returnUrl
});

let submit = () => {
  form.post("/auth/login");
}

router.on('invalid', (event) => {
  console.log(`An invalid Inertia response was received.`)
  console.log(event.detail.response)
})

</script>

<template>
  <section class="flex items-center flex-1 w-full overflow-x-hidden min-h-screen">

    <div class="w-full max-w-sm mx-auto overflow-hidden bg-white rounded-lg shadow-md dark:bg-gray-800">
      <div class="px-6 py-4">
        <h3 class="mt-3 text-xl font-medium text-center text-gray-600 dark:text-gray-200">Welcome Back</h3>

        <p class="mt-1 text-center text-gray-500 dark:text-gray-400">Login or create account</p>

        <form @submit.prevent="submit">
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
                type="password" placeholder="Password" aria-label="Password" autocomplete="current-password"/>
          </div>

          <div class="flex items-center justify-between mt-4">
            <a href="#" class="text-sm text-gray-600 dark:text-gray-200 hover:text-gray-500">Forget Password?</a>

            <button
                class="px-6 py-2 text-sm font-medium tracking-wide text-white capitalize transition-colors duration-300 transform bg-blue-500 rounded-lg hover:bg-blue-400 focus:outline-none focus:ring focus:ring-blue-300 focus:ring-opacity-50">
              Sign In
            </button>
          </div>
        </form>
      </div>

      <div class="flex items-center justify-center py-4 text-center bg-gray-50 dark:bg-gray-700">
        <span class="text-sm text-gray-600 dark:text-gray-200">Don't have an account? </span>
        <Link href="/auth/register" class="mx-2 text-sm font-bold text-blue-500 dark:text-blue-400 hover:underline">
          Register
        </Link>
      </div>
    </div>
  </section>
  <Toastr :errors="errors" v-if="Object.keys(errors).length > 1">
    Error:
  </Toastr>

</template>

