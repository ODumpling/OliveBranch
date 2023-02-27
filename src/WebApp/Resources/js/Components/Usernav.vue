<template>
  <Menu as="div" class="relative inline-block ">
    <!-- Dropdown toggle button -->
    <MenuButton
        class="relative z-10 flex items-center p-2 text-sm text-gray-600 bg-white border border-transparent rounded-md focus:border-blue-500 focus:ring-opacity-40 dark:focus:ring-opacity-40 focus:ring-blue-300 dark:focus:ring-blue-400 focus:ring dark:text-white dark:bg-gray-800 focus:outline-none">
      <span class="mx-1">
        {{ user.username }}
      </span>
      <ChevronDownIcon class="text-gray-700 dark:text-gray-200 h-4 w-4 ml-2"/>
    </MenuButton>

    <!-- Dropdown menu -->
    <Transition
        x-transition:enter="transition ease-out duration-100"
        x-transition:enter-start="opacity-0 scale-90"
        x-transition:enter-end="opacity-100 scale-100"
        x-transition:leave="transition ease-in duration-100"
        x-transition:leave-start="opacity-100 scale-100"
        x-transition:leave-end="opacity-0 scale-90"

    >
      <MenuItems
          class="absolute right-0 z-20 w-56 py-2 mt-2 overflow-hidden origin-top-right bg-white rounded-md shadow-xl dark:bg-gray-800">

        <a href="#"
           class="flex items-center p-3 -mt-2 text-sm text-gray-600 transition-colors duration-300 transform dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 dark:hover:text-white">
          <div class="mx-1">
            <h1 class="text-sm font-semibold text-gray-700 dark:text-gray-200">{{ user.username }}</h1>
            <p class="text-sm text-gray-500 dark:text-gray-400">{{ user.email }}</p>
          </div>
        </a>

        <hr class="border-gray-200 dark:border-gray-700 ">

          <MenuItem v-for="item in usernav" :key="item.name">
            <Link :href="item.href"
                  class="block px-4 py-3 text-sm text-gray-600 capitalize transition-colors duration-300 transform dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 dark:hover:text-white">
              {{ item.name }}
            </Link>
          </MenuItem>


        <hr class="border-gray-200 dark:border-gray-700 ">
        <MenuItem>
          <Link href="auth/logout"
                method="post"
                as="button"
                class="w-full text-left block px-4 py-3 text-sm text-gray-600 capitalize transition-colors duration-300 transform dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 dark:hover:text-white">
            Sign Out
          </Link>
        </MenuItem>
      </MenuItems>
    </Transition>
  </Menu>
</template>

<script setup>
import {Menu, MenuButton, MenuItem, MenuItems} from "@headlessui/vue";
import {ChevronDownIcon} from '@heroicons/vue/24/outline';


defineProps({
  user: Object,
});

const usernav = [
  // {name: "View Profile", href: "#"},
  // {name: "Settings", href: "#"},
]

</script>

