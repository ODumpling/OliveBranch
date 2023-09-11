<template>
  <main>
    <Disclosure as="nav" class="relative bg-white shadow dark:bg-gray-800" v-slot="{open}">
      <div class="container px-6 py-4 mx-auto">
        <div class="lg:flex lg:items-center lg:justify-between">
          <div class="flex items-center justify-between">
            <Link href="/">
              <img class="w-auto h-6 sm:h-7" src="https://merakiui.com/images/full-logo.svg" alt="">
            </Link>

            <div class="flex flex-col -mx-6 lg:flex-row lg:items-center lg:mx-8">
              <Link v-for="item in navigation" :key="item.name" :href="item.href"
                    class="px-3 py-2 mx-3 mt-2 text-gray-700 transition-colors duration-300 transform rounded-md lg:mt-0 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-700">
                {{ item.name }}
              </Link>
            </div>
            
            
            <!-- Mobile menu button -->
            <div class="flex lg:hidden">
              <DisclosureButton type="button"
                                class="text-gray-500 dark:text-gray-200 hover:text-gray-600 dark:hover:text-gray-400 focus:outline-none focus:text-gray-600 dark:focus:text-gray-400"
                                aria-label="toggle menu">
                <svg v-if="!open" xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24"
                     stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M4 8h16M4 16h16"/>
                </svg>

                <svg v-if="open" xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24"
                     stroke="currentColor" stroke-width="2">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"/>
                </svg>
              </DisclosureButton>
            </div>
          </div>
          
          

          <!-- Mobile Menu open: "block", Menu closed: "hidden" -->
          <div :class="[open ? 'translate-x-0 opacity-100 ' : 'opacity-0 -translate-x-full']"
               class="absolute inset-x-0 z-20 w-full px-6 py-4 transition-all duration-300 ease-in-out bg-white dark:bg-gray-800 lg:mt-0 lg:p-0 lg:top-0 lg:relative lg:bg-transparent lg:w-auto lg:opacity-100 lg:translate-x-0 lg:flex lg:items-center">
            

            <div class="flex items-center mt-4 lg:mt-0">
              <Usernav :user="user"/>
            </div>
          </div>
        </div>
      </div>
    </Disclosure>
    <main class="container mx-auto">
      <slot>
        <h1>Content</h1>
      </slot>
    </main>

  </main>

</template>

<script setup>
import {Disclosure, DisclosureButton, DisclosurePanel} from '@headlessui/vue'
import {
  HomeIcon,
  ChevronDownIcon,
} from '@heroicons/vue/24/outline'
import {computed} from "vue";
import Usernav from "../Components/Usernav.vue";

const navigation = [
  {name: 'Home', href: '#', icon: HomeIcon, current: true},
]

const props = defineProps({
  auth: Object,
});

const user = computed(() => props.auth.user)

</script>




