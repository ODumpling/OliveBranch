<template>

  <div>
    <!-- Content area -->
    <main class="flex-1">
      <div class="px-4 sm:px-6 lg:px-0">
        <h1 class="text-3xl font-bold tracking-tight text-gray-900">Settings</h1>
      </div>
      <div class="px-4 sm:px-6 lg:px-0">
        <div class="py-6">
          <!-- Tabs -->
          <div class="lg:hidden">
            <label class="sr-only" for="selected-tab">Select a tab</label>
            <select id="selected-tab" class="mt-1 block w-full rounded-md border-gray-300 py-2 pl-3 pr-10 text-base focus:border-blue-500 focus:outline-none focus:ring-blue-500 sm:text-sm"
                    name="selected-tab">
              <option v-for="tab in tabs" :key="tab.name" :selected="tab.current">{{ tab.name }}</option>
            </select>
          </div>
          <div class="hidden lg:block">
            <div class="border-b border-gray-200">
              <nav class="-mb-px flex space-x-8">
                <a v-for="tab in tabs" :key="tab.name" :class="[tab.current ? 'border-blue-500 text-blue-600' : 'border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700', 'whitespace-nowrap py-4 px-1 border-b-2 font-medium text-sm']"
                   :href="tab.href">{{
                    tab.name
                  }}</a>
              </nav>
            </div>
          </div>

          <!-- Description list with inline editing -->
          <div class="mt-10 divide-y divide-gray-200">
            <section class="flex justify-between">
              <div class="space-y-1">
                <h3 class="text-lg font-medium leading-6 text-gray-900">Profile</h3>
                <p class="max-w-2xl text-sm text-gray-500">This information will be displayed publicly so be
                  careful what you share.</p>
              </div>

              <span class="ml-4 flex-shrink-0">
                <button class="rounded-md font-medium bg-blue-500 text-white py-2 px-4 hover:text-black focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                        type="button">Update</button>
            </span>
            </section>

            <div class="mt-6">
              <dl class="divide-y divide-gray-200">
                <div class="py-4 sm:grid sm:grid-cols-3 sm:gap-4 sm:py-5">
                  <dt class="text-sm font-medium text-gray-500">Name</dt>
                  <dd class="mt-1 flex text-sm text-gray-900 sm:col-span-2 sm:mt-0">
                    <span class="flex-grow">{{ user.username }}</span>
                  </dd>
                </div>

                <div class="py-4 sm:grid sm:grid-cols-3 sm:gap-4 sm:py-5 sm:pt-5">
                  <dt class="text-sm font-medium text-gray-500">Email</dt>
                  <dd class="mt-1 flex text-sm text-gray-900 sm:col-span-2 sm:mt-0">
                    <span class="flex-grow">{{ user.email }}</span>
                    
                  </dd>
                </div>

                <div class="py-4 sm:grid sm:grid-cols-3 sm:gap-4 sm:py-5 sm:pt-5">
                  <dt class="text-sm font-medium text-gray-500">Password</dt>
                  <dd class="mt-1 flex text-sm text-gray-900 sm:col-span-2 sm:mt-0">
                    <span class="flex-grow">**********</span>
                  </dd>
                </div>

              </dl>
            </div>
          </div>
        </div>
      </div>
    </main>
    
  </div>
</template>

<script setup>
import {computed} from 'vue';

const props = defineProps({
  auth: Object,
});

const user = computed(() => props.auth.user)

const tabs = [
  {name: 'Profile', href: '#', current: true},
  {name: 'Notifications', href: '#', current: false},
]

</script>